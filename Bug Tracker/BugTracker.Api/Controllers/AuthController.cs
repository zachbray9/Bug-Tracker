using BugTracker.Api.Models.Requests;
using BugTracker.Api.Services.TokenServices;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BugTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> UserManager;
        private readonly AuthTokenService AuthTokenService;

        public AuthController(UserManager<User> userManager, AuthTokenService authTokenService)
        {
            UserManager = userManager;
            AuthTokenService = authTokenService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("The information you entered is invalid. Please try again.");
            }

            if(registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            User newUser = new User
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                UserName = registerRequest.Email
            };

            var result = await UserManager.CreateAsync(newUser, registerRequest.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            UserDTO userDTO = CreateUserDTO(newUser);

            await SetRefreshToken(newUser);
            return Ok(userDTO);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("The information you entered is invalid. Please try again.");
            }

            User? user = await UserManager.FindByEmailAsync(loginRequest.Email);
            if(user == null)
            {
                return Unauthorized("No user with this email exists.");
            }

            bool isCorrectPassword = await UserManager.CheckPasswordAsync(user, loginRequest.Password);
            if(isCorrectPassword == false)
            {
                return Unauthorized("Incorrect password.");
            }

            UserDTO userDTO = CreateUserDTO(user);

            await SetRefreshToken(user);
            return Ok(userDTO);
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> CurrentUser()
        {
            User user = await UserManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if(user == null)
                return BadRequest("User does not exist.");

            UserDTO userDTO = CreateUserDTO(user);

            return Ok(userDTO);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = await UserManager.Users.Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
                return Unauthorized();

            var oldToken = user.RefreshTokens.SingleOrDefault(u => u.Token == refreshToken);
            if (oldToken != null && !oldToken.IsActive)
                return Unauthorized();

            if (oldToken != null)
                oldToken.Revoked = DateTime.UtcNow;

            return Ok(CreateUserDTO(user));

        }

        [Authorize]
        [HttpDelete("[action]")]
        public IActionResult Logout()
        {
            return Ok("User has successfully logged out.");
        }

        //helpers
        private async Task SetRefreshToken(User user)
        {
            var refreshToken = AuthTokenService.GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);

            await UserManager.UpdateAsync(user);

            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOption);
        }

        private UserDTO CreateUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateJoined = user.DateJoined,
                AuthToken = AuthTokenService.CreateToken(user),
                ProfilePictureUrl = user.ProfilePictureUrl,
            };
        }
    }
}

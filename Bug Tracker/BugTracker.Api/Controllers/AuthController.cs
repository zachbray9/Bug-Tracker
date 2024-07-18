using BugTracker.Api.Models.Requests;
using BugTracker.Api.Services.TokenServices;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                UserName = registerRequest.Email,
                DateJoined = DateTime.UtcNow,
            };

            var result = await UserManager.CreateAsync(newUser, registerRequest.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            UserDTO userDTO = CreateUserDTO(newUser);

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

            return Ok(userDTO);
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> CurrentUser()
        {
            User user = await UserManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            UserDTO userDTO = CreateUserDTO(user);

            return Ok(userDTO);
        }

        [Authorize]
        [HttpDelete("[action]")]
        public IActionResult Logout()
        {
            return Ok("User has successfully logged out.");
        }

        //helpers
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

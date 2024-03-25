using BugTracker.Api.Models.Requests;
using BugTracker.Api.Models.Responses;
using BugTracker.Api.Services.Authenticators;
using BugTracker.Api.Services.TokenDbServices;
using BugTracker.Api.Services.TokenGenerators;
using BugTracker.Api.Services.TokenValidators;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.Auth;
using BugTracker.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace BugTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly RefreshTokenValidator RefreshTokenValidator;
        private readonly RefreshTokenService RefreshTokenService;
        private readonly Authenticator Authenticator;
        private readonly IPasswordHasher PasswordHasher;

        public AuthController(BugTrackerDbContext dbContext, RefreshTokenValidator refreshTokenValidator, RefreshTokenService refreshTokenService, Authenticator authenticator, IPasswordHasher passwordHasher)
        {
            DbContext = dbContext;
            RefreshTokenValidator = refreshTokenValidator;
            RefreshTokenService = refreshTokenService;
            Authenticator = authenticator;
            PasswordHasher = passwordHasher;
        }

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

            if (await DbContext.Users.AnyAsync(u => u.Email == registerRequest.Email))
            {
                return Conflict("A user with this email already exists.");
            }

            string passwordHash = PasswordHasher.HashPassword(registerRequest.Password);

            User newUser = new User
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                PasswordHash = passwordHash
            };

            EntityEntry entity = await DbContext.Users.AddAsync(newUser);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("The information you entered is invalid. Please try again.");
            }

            User? user = await DbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            if(user == null)
            {
                return Unauthorized("No user with this email exists.");
            }

            PasswordVerificationResult result = PasswordHasher.VerifyHashedPassword(user.PasswordHash, loginRequest.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Incorrect password.");
            }

            AuthenticatedUserResponse response = await Authenticator.Authenticate(user);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //check to make sure refesh token signature is valid and token is not expired
            bool isValidRefreshToken = RefreshTokenValidator.ValidateRefreshToken(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
                return BadRequest("Your refresh token is invalid.");

            RefreshToken? refreshTokenDTO = await RefreshTokenService.GetByToken(refreshRequest.RefreshToken);
            if(refreshTokenDTO == null)
            {
                return BadRequest("Invalid refresh token.");
            }

            //Delete refresh token after retrieving it so it can't be used more than once.
            await RefreshTokenService.Delete(refreshTokenDTO.Id);

            User? user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == refreshTokenDTO.UserId);
            if(user == null)
            {
                return NotFound("User not found.");
            }

            //if user has valid refresh token, then give them a new access token and refresh token for new login
            AuthenticatedUserResponse response = await Authenticator.Authenticate(user);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");

            if(!int.TryParse(rawUserId, out int userId))
            {
                return Unauthorized();
            }
        }
    }
}

using BugTracker.Api.Models.Requests;
using BugTracker.Api.Models.Responses;
using BugTracker.Api.Services.TokenGenerators;
using BugTracker.Domain.Models;
using BugTracker.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BugTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly AccessTokenGenerator AccessTokenGenerator;
        private readonly IPasswordHasher PasswordHasher;

        public AuthController(BugTrackerDbContext dbContext, AccessTokenGenerator accessTokenGenerator, IPasswordHasher passwordHasher)
        {
            DbContext = dbContext;
            PasswordHasher = passwordHasher;
            AccessTokenGenerator = accessTokenGenerator;
        }

        [HttpPost("register")]
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

        [HttpPost("login")]
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

            string accessToken = AccessTokenGenerator.GenerateAccessToken(user);
            return Ok(new AuthenticatedUserResponse
            {
                AccessToken = accessToken,
            });
        }
    }
}

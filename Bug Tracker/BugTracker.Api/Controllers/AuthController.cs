using BugTracker.Api.Attributes;
using BugTracker.Api.Models.Requests;
using BugTracker.Api.Services.SessionServices;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.Auth;
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
        private readonly IPasswordHasher PasswordHasher;
        private readonly SessionDbService SessionService;

        public AuthController(BugTrackerDbContext dbContext, IPasswordHasher passwordHasher, SessionDbService sessionService)
        {
            DbContext = dbContext;
            PasswordHasher = passwordHasher;
            SessionService = sessionService;
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

            //AuthenticatedUserResponse response = await Authenticator.Authenticate(user);
            AgileSession session = await SessionService.CreateSession(user);
            Response.Cookies.Append("AgileSessionId", session.Id.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(30),
                //SameSite = SameSiteMode.Strict,
                //Domain = ".localhost:7226"
            });

            return Ok();
        }

        [CheckAuthorization]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Logout()
        {
            string? sessionId = HttpContext.Request.Cookies["AgileSessionId"];
            if (sessionId == null)
                return Unauthorized();

            AgileSession? session = await DbContext.Sessions.FirstOrDefaultAsync(s => s.Id.ToString() == sessionId);
            if (session == null)
                return Unauthorized();

            await SessionService.DeleteSession(sessionId);
            HttpContext.Response.Cookies.Delete("AgileSessionId");
            return Ok("User has successfully logged out.");
        }
    }
}

using BugTracker.Api.Attributes;
using BugTracker.Api.Models.Requests;
using BugTracker.Api.Services.SessionServices;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.Auth;
using BugTracker.Domain.Models.DTOs;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly UserManager<User> UserManager;
        private readonly SessionDbService SessionService;

        public AuthController(BugTrackerDbContext dbContext, UserManager<User> userManager, SessionDbService sessionService)
        {
            DbContext = dbContext;
            UserManager = userManager;
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

            User newUser = new User
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                UserName = registerRequest.Email,
                DateJoined = DateTime.UtcNow
            };

            var result = await UserManager.CreateAsync(newUser, registerRequest.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

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

            //AgileSession session = await SessionService.CreateSession(user);
            //Response.Cookies.Append("AgileSessionId", session.Id.ToString(), new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    Expires = DateTime.UtcNow.AddDays(30),
            //    //SameSite = SameSiteMode.Strict,
            //    //Domain = ".localhost:7226"
            //});

            UserDTO userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateJoined = user.DateJoined
            };

            return Ok(userDTO);
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

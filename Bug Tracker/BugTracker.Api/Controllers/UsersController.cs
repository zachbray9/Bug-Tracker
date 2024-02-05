using BugTracker.Domain.Models;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BugTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly BugTrackerDbContext DbContext;

        public UsersController(BugTrackerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet]
        [Route ("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            User? user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}

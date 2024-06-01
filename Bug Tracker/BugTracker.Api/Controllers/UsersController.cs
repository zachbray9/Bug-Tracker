using AutoMapper;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

namespace BugTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly UserManager<User> UserManager;
        private readonly IMapper Mapper;

        public UsersController(BugTrackerDbContext dbContext, UserManager<User> userManager, IMapper mapper)
        {
            DbContext = dbContext;
            UserManager = userManager;
            Mapper = mapper;
        }

        [HttpGet]
        [Route ("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            User? user = await UserManager.FindByIdAsync(id);
                    
            if(user == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserDTO>(user));
        }

        [HttpGet]
        [Route ("byEmail/{email}")]
        public async Task<IActionResult> GetByEmail([FromRoute] string email)
        {
            User? user = await UserManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserDTO>(user));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<User>? users = await DbContext.Users.ToListAsync();
            return Ok(Mapper.Map<List<UserDTO>>(users));
        }

        [HttpGet]
        [Route("Projects")]
        public async Task<IActionResult> GetAllProjectsFromUser()
        {
            var user = await UserManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return Unauthorized();
            }

            List<ProjectDTO?> projects = await DbContext.Projects
                .Where(p => p.Users.Any(pu => pu.UserId.Equals(user.Id)))
                .Select(p => Mapper.Map<ProjectDTO>(p))
                .ToListAsync();

            return Ok(projects);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO userDTO)
        {
            if(await DbContext.Users.AnyAsync(u => u.Email == userDTO.Email))
                return Conflict("A user with this email already exists.");

            User? newUser = new User();
            newUser = Mapper.Map<User>(userDTO);

            EntityEntry<User> newUserEntity = await DbContext.Users.AddAsync(newUser);
            await DbContext.SaveChangesAsync();

            return Created($"~/api/Users/{userDTO.Id}", Mapper.Map<UserDTO>(newUserEntity.Entity));
        }

        [HttpPut]
        [Route("{userId:guid}")]
        public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] UserDTO userDTO)
        {
            if (await DbContext.Users.AnyAsync(u => u.Email == userDTO.Email && !u.Id.Equals(userId)))
                return Conflict("A user with this email already exists.");

            User? user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
            if(user == null)
                return NotFound();

            Mapper.Map(userDTO, user);
            await DbContext.SaveChangesAsync();

            return Ok(Mapper.Map<UserDTO>(user));
        }

        [HttpDelete]
        [Route ("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            User? user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (user == null)
                return NotFound();

            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();

            return Ok("User was successfully deleted.");
        }

        
    }
}

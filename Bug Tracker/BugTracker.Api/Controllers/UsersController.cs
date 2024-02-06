using AutoMapper;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BugTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly IMapper Mapper;

        public UsersController(BugTrackerDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        [HttpGet]
        [Route ("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            User? user = await DbContext.Users
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.AuthoredTickets)
                        .ThenInclude(t => t.Comments)
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.AssignedTickets)
                        .ThenInclude(t => t.Comments)
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.Comments)
                .FirstOrDefaultAsync(u => u.Id == id);
                    
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
            User? user = await DbContext.Users
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.AuthoredTickets)
                        .ThenInclude(t => t.Comments)
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.AssignedTickets)
                        .ThenInclude(t => t.Comments)
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.Comments)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserDTO>(user));
        }

        [HttpGet]
        [Route ("byName/{fullName}")]
        public async Task<IActionResult> GetByFullName([FromRoute] string fullName)
        {
            User? user = await DbContext.Users
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.AuthoredTickets)
                        .ThenInclude(t => t.Comments)
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.AssignedTickets)
                        .ThenInclude(t => t.Comments)
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.Comments)
                .FirstOrDefaultAsync ((u) => (u.FirstName + u.LastName) == fullName);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserDTO>(user));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<User>? users = await DbContext.Users
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.AuthoredTickets)
                        .ThenInclude(t => t.Comments)
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.AssignedTickets)
                        .ThenInclude(t => t.Comments)
                .Include(u => u.ProjectUsers)
                    .ThenInclude(pu => pu.Comments)
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("The user object you are trying to pass is null.");

            if(await DbContext.Users.AnyAsync(u => u.Email == userDTO.Email))
                return Conflict("A user with this email already exists.");

            User? user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == userDTO.Id);
            if (user != null)
                return Conflict("A user with this id already exists.");

            user = new User
            {
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PasswordHash = userDTO.PasswordHash,
                DateJoined = userDTO.DateJoined
            };
            EntityEntry<User> newUser = await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();

            return Created($"~/api/Users/{userDTO.Id}", Mapper.Map<UserDTO>(newUser.Entity));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
        {
            if(userDTO == null)
                return BadRequest("The user object you are trying to pass is null.");

            if (await DbContext.Users.AnyAsync(u => u.Email == userDTO.Email))
                return Conflict("A user with this email already exists.");

            User? user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == userDTO.Id);
            if(user == null)
                return NotFound();

            user.Id = userDTO.Id;
            user.Email = userDTO.Email;
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;

            EntityEntry<User> updatedUser = DbContext.Users.Update(user);
            await DbContext.SaveChangesAsync();

            return Ok(Mapper.Map<UserDTO>(updatedUser.Entity));
        }

        [HttpDelete]
        [Route ("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            User? user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();

            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();

            return Ok("User was successfully deleted.");
        }

        
    }
}

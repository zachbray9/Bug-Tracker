using AutoMapper;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Models;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectUsersController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly IMapper Mapper;

        public ProjectUsersController(BugTrackerDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            ProjectUser? projectUser = await DbContext.ProjectUsers
                .Include(pu => pu.AuthoredTickets)
                    .ThenInclude(t => t.Comments)
                .Include(pu => pu.AssignedTickets)
                    .ThenInclude(t => t.Comments)
                .Include(pu => pu.Comments)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (projectUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProjectUserDTO>(projectUser));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProjectUser>? projectUsers = await DbContext.ProjectUsers
                .Include(pu => pu.AuthoredTickets)
                    .ThenInclude(t => t.Comments)
                .Include(pu => pu.AssignedTickets)
                    .ThenInclude(t => t.Comments)
                .Include(pu => pu.Comments)
                .ToListAsync();

            List<ProjectUserDTO> projectUserDTOs = projectUsers.Select(pu => Mapper.Map<ProjectUserDTO>(pu)!).ToList();

            return Ok(projectUserDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectUserDTO projectUserDTO)
        {
            if (projectUserDTO == null)
                return BadRequest("The user object you are trying to pass is null.");

            if (await DbContext.ProjectUsers.AnyAsync(pu => pu.Id == projectUserDTO.Id && pu.ProjectId == projectUserDTO.ProjectId))
                return Conflict("This user is already a member of this project.");

            ProjectUser projectUser = new ProjectUser
            {
                ProjectId = projectUserDTO.ProjectId,
                UserId = projectUserDTO.UserId,
                Role = projectUserDTO.Role
            };
            EntityEntry<ProjectUser> newProjectUser = await DbContext.ProjectUsers.AddAsync(projectUser);
            await DbContext.SaveChangesAsync();

            return Created($"~/api/Users/{projectUserDTO.Id}", Mapper.Map<ProjectUserDTO>(newProjectUser.Entity));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProjectUserDTO projectUserDTO)
        {
            if (projectUserDTO == null)
                return BadRequest("The user object you are trying to pass is null.");

            ProjectUser? projectUser = await DbContext.ProjectUsers.FirstOrDefaultAsync(u => u.Id == projectUserDTO.Id);
            if (projectUser == null)
                return NotFound();

            projectUser.Id = projectUserDTO.Id;
            projectUser.Role = projectUserDTO.Role;

            EntityEntry<ProjectUser> updatedProjectUser = DbContext.ProjectUsers.Update(projectUser);
            await DbContext.SaveChangesAsync();

            return Ok(Mapper.Map<ProjectUserDTO>(updatedProjectUser.Entity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            ProjectUser? projectUser = await DbContext.ProjectUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (projectUser == null)
                return NotFound();

            DbContext.ProjectUsers.Remove(projectUser);
            await DbContext.SaveChangesAsync();

            return Ok("ProjectUser was successfully deleted.");
        }
    }
}

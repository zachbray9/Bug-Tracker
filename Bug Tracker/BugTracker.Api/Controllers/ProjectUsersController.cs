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
    [Route("api")]
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
        [Route("Projects/{projectId:int}/Users/{userId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int projectId, [FromRoute] int userId)
        {
            ProjectUser? projectUser = await DbContext.ProjectUsers.Include(pu => pu.Project).Include(pu => pu.User).FirstOrDefaultAsync(pu => pu.UserId == userId && pu.ProjectId == projectId);

            if (projectUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProjectUserDTO>(projectUser));
        }

        [HttpGet]
        [Route("ProjectUsers")]
        public async Task<IActionResult> GetAll()
        {
            List<ProjectUser>? projectUsers = await DbContext.ProjectUsers.Include(pu => pu.Project).Include(pu => pu.User).ToListAsync();
            return Ok(Mapper.Map<List<ProjectUserDTO>>(projectUsers));
        }

        [HttpPost]
        [Route("Projects/{projectId:int}/Users")]
        public async Task<IActionResult> Create([FromRoute] int projectId, [FromBody] ProjectUserDTO projectUserDTO)
        {
            if (!await DbContext.Projects.AnyAsync(p => p.Id == projectId))
                return NotFound("No project with that project id exists.");

            if (await DbContext.ProjectUsers.AnyAsync(pu => pu.UserId == projectUserDTO.UserId && pu.ProjectId == projectId))
                return Conflict("This user is already a member of this project.");

            ProjectUser? projectUser = Mapper.Map<ProjectUser>(projectUserDTO);
            EntityEntry<ProjectUser> newProjectUser = await DbContext.ProjectUsers.AddAsync(projectUser);
            await DbContext.SaveChangesAsync();

            return Created("~/api/Projects/{projectId}/Users/{userId}", await GetById(newProjectUser.Entity.ProjectId, newProjectUser.Entity.UserId));
        }

        [HttpPut]
        [Route("Projects/{projectId:int}/Users/{userId:int}")]
        public async Task<IActionResult> Update([FromRoute] int projectId, [FromRoute] int userId, [FromBody] ProjectUserDTO projectUserDTO)
        {
            ProjectUser? projectUser = await DbContext.ProjectUsers.FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);
            if (projectUser == null)
                return NotFound("No Project User exists with this ProjectId and UserId.");

            Mapper.Map(projectUserDTO, projectUser);
            await DbContext.SaveChangesAsync();

            return Ok(await GetById(projectId, projectUser.UserId));
        }

        [HttpDelete]
        [Route("Projects/{projectId:int}/Users/{userId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int projectId, [FromRoute] int userId)
        {
            ProjectUser? projectUser = await DbContext.ProjectUsers.FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);
            if (projectUser == null)
                return NotFound("No Project User exists with this ProjectId and UserId.");

            DbContext.ProjectUsers.Remove(projectUser);
            await DbContext.SaveChangesAsync();

            return Ok("ProjectUser was successfully deleted.");
        }
    }
}

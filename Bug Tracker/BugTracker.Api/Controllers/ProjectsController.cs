using AutoMapper;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Models;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BugTracker.Api.Models.Requests;
using System.Security.Claims;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using BugTracker.Domain.Enumerables;

namespace BugTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly UserManager<User> UserManager;
        private readonly IMapper Mapper;

        public ProjectsController(BugTrackerDbContext dbContext, UserManager<User> userManager, IMapper mapper)
        {
            DbContext = dbContext;
            UserManager = userManager;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<ProjectDTO>? projectDtos = await DbContext.Projects
                .Where(p => p.Users.Any(pu => pu.UserId.Equals(userId)))
                .ProjectTo<ProjectDTO>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(projectDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest createProjectRequest)
        {
            Project? project = new Project
            {
                Name = createProjectRequest.Name,
                Description = createProjectRequest.Description,
                DateStarted = DateTime.UtcNow,
            };
            
            EntityEntry<Project> newProject = await DbContext.Projects.AddAsync(project);

            ProjectUser projectCreator = new ProjectUser
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ProjectId = newProject.Entity.Id,
                Role = Domain.Enumerables.ProjectRole.Owner
            };

            await DbContext.ProjectUsers.AddAsync(projectCreator);

            await DbContext.SaveChangesAsync();

            ProjectDTO? projectDto = await DbContext.Projects.ProjectTo<ProjectDTO>(Mapper.ConfigurationProvider).SingleOrDefaultAsync(p => p.Id == newProject.Entity.Id);

            return Created($"~/api/Projects/{newProject.Entity.Id}", projectDto);
        }

        [Authorize("IsProjectAdmin")]
        [HttpPost("{projectId:guid}/[action]")]
        public async Task<IActionResult> AddUser([FromRoute] Guid projectId, [FromBody] AddUserToProjectRequest request)
        {
            //checks if the user that is being added exists
            User user = await UserManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                return NotFound("A user with this email could not be found.");
            }

            //checks if the project that you are trying to add someone to exists
            Project? project = await DbContext.Projects.Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == projectId);
            if(project == null)
            {
                return NotFound("A project with this id could not be found.");
            }

            //checks if the user that is being added is already on this project
            if(project.Users.Any(pu => pu.UserId == user.Id))
            {
                return Conflict("This user is already a member of this project");
            }

            project.Users.Add(new ProjectUser
            {
                UserId = user.Id,
                ProjectId = project.Id,
                Role = ProjectRoleExtensions.ParseProjectRole(request.Role)
            });

            await DbContext.SaveChangesAsync();

            return Ok(await DbContext.ProjectUsers.ProjectTo<ProjectParticipant>(Mapper.ConfigurationProvider).SingleOrDefaultAsync(pu => pu.ProjectId == project.Id && pu.UserId == user.Id));
        }

        [Authorize("IsProjectAdmin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProjectDTO projectDTO)
        {
            Project? project = await DbContext.Projects.FirstOrDefaultAsync(u => u.Id == projectDTO.Id);
            if (project == null)
                return NotFound();

            Mapper.Map(projectDTO, project);
            await DbContext.SaveChangesAsync();

            return Ok(Mapper.Map<ProjectDTO>(project));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Project? project = await DbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
                return NotFound();

            DbContext.Projects.Remove(project);
            await DbContext.SaveChangesAsync();

            return Ok("Project was successfully deleted.");
        }
    }
}

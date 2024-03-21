using AutoMapper;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Models;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BugTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly IMapper Mapper;

        public ProjectsController(BugTrackerDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Project? project = await DbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProjectDTO>(project));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Project>? projects = await DbContext.Projects.ToListAsync();

            List<ProjectDTO> projectDTOs = projects.Select(p => Mapper.Map<ProjectDTO>(p)!).ToList();

            return Ok(projectDTOs);
        }

        [HttpGet]
        [Route("{projectId:int}/Users")]
        public async Task<IActionResult> GetAllUsersOnProject([FromRoute] int projectId)
        {
            Project? project = await DbContext.Projects.Include(p => p.Users).ThenInclude(pu => pu.User).FirstOrDefaultAsync(p => p.Id.Equals(projectId));
            if(project == null)
            {
                return NotFound($"No project with an id of {projectId} exists.");
            }

            List<ProjectUserDTO>? users = Mapper.Map<List<ProjectUserDTO>>(project.Users);
            
            return Ok(users);

        }

        [HttpGet]
        [Route("{projectId:int}/Tickets")]
        public async Task<IActionResult> GetAllTicketsOnProject([FromRoute] int projectId)
        {
            Project? project = await DbContext.Projects
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Author)
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Assignee)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound($"No project with the id {projectId} exists.");
            }
         
            List<TicketDTO>? ticketDTOs = Mapper.Map<List<TicketDTO>>(project.Tickets);
            return Ok(ticketDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectDTO projectDTO)
        {
            Project? project = new Project();
            project = Mapper.Map<Project>(projectDTO);
            
            EntityEntry<Project> newProject = await DbContext.Projects.AddAsync(project);
            await DbContext.SaveChangesAsync();

            return Created($"~/api/Projects/{projectDTO.Id}", Mapper.Map<ProjectDTO>(newProject.Entity));
        }

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
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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

﻿using AutoMapper;
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
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<ProjectDTO>? projectDtos = await DbContext.Projects
                .Where(p => p.Users.Any(pu => pu.UserId.Equals(userId)))
                .ProjectTo<ProjectDTO>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(projectDtos);
        }

        [HttpGet]
        [Route("{projectId:guid}/Users")]
        public async Task<IActionResult> GetAllParticipantsOnProject([FromRoute] Guid projectId)
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
        [Route("{projectId:guid}/Tickets")]
        public async Task<IActionResult> GetAllTicketsOnProject([FromRoute] Guid projectId)
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
                Role = Domain.Enumerables.ProjectRole.Administrator
            };

            await DbContext.ProjectUsers.AddAsync(projectCreator);

            await DbContext.SaveChangesAsync();

            return Created($"~/api/Projects/{newProject.Entity.Id}", Mapper.Map<ProjectDTO>(newProject.Entity));
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

﻿using AutoMapper;
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
            Project project = new Project
            {
                Name = projectDTO.Name,
                Description = projectDTO.Description,
                DateStarted = projectDTO.DateStarted,
            };
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

            project.Name = projectDTO.Name;
            project.Description = projectDTO.Description;

            EntityEntry<Project> updatedProject = DbContext.Projects.Update(project);
            await DbContext.SaveChangesAsync();

            return Ok(Mapper.Map<ProjectDTO>(updatedProject.Entity));
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

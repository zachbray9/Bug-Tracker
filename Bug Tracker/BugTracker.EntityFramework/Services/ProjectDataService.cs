using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BugTracker.EntityFramework.Services
{
    public class ProjectDataService : IProjectService
    {
        private readonly BugTrackerDbContextFactory ContextFactory;

        public ProjectDataService(BugTrackerDbContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<Project> Create(Project entity)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                EntityEntry<Project> createdResult = await context.Set<Project>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                Project? entity = await context.Set<Project>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<Project>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Project> Get(int id)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                Project? entity = await context.Set<Project>().Include(p => p.ProjectUsers).Include(p => p.Tickets).FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                IEnumerable<Project> entities = await context.Set<Project>().Include(p => p.ProjectUsers).Include(p => p.Tickets).ToListAsync();

                return entities;
            }
        }

        public async Task<Project> Update(int id, Project entity)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<Project>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}

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
    public class ProjectUserDataService : IProjectUserService
    {
        private readonly BugTrackerDbContextFactory ContextFactory;

        public ProjectUserDataService(BugTrackerDbContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<ProjectUser> Create(ProjectUser entity)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                EntityEntry<ProjectUser> createdResult = await context.Set<ProjectUser>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                ProjectUser? entity = await context.Set<ProjectUser>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<ProjectUser>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<ProjectUser> Get(int id)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                ProjectUser? entity = await context.Set<ProjectUser>().Include(pu => pu.Tickets).Include(pu => pu.Comments).FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<ProjectUser>> GetAll()
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                IEnumerable<ProjectUser> entities = await context.Set<ProjectUser>().Include(pu => pu.Tickets).Include(pu => pu.Comments).ToListAsync();

                return entities;
            }
        }

        public async Task<ProjectUser> Update(int id, ProjectUser entity)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<ProjectUser>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<ProjectUser> GetByFindMethod(int id)
        {
            using(BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                ProjectUser? entity = await context.Set<ProjectUser>().FindAsync(id);
                return entity;       
            }
        }
    }
}

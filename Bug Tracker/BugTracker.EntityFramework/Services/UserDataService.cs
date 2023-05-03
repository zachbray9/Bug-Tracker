using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BugTracker.EntityFramework.Services
{
    public class UserDataService : IUserService
    {
        private readonly BugTrackerDbContextFactory ContextFactory;

        public UserDataService(BugTrackerDbContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<User> Create(User entity)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                EntityEntry<User> createdResult = await context.Set<User>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                User? entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<User>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                IEnumerable<User> entities = await context.Set<User>().Include(u => u.ProjectUsers).ToListAsync();

                return entities;
            }
        }

        public async Task<User> Get(int id)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                //testing eager loading by adding the .Include() method
                User? entity = await context.Set<User>().Include(u => u.ProjectUsers).FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }


        public async Task<User> GetByEmail(string email)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                //testing eager loading by adding the .Include method
                User? entity = await context.Set<User>().Include(u => u.ProjectUsers).FirstOrDefaultAsync((e) => e.Email == email);
                return entity;
            }
        }

        public async Task<User> Update(int id, User entity)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<User>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }



    }
}

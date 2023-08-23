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
        private readonly BugTrackerDbContext context;

        public UserDataService(BugTrackerDbContext context)
        {
            this.context = context;
        }

        public async Task<User> Create(User entity)
        {
                EntityEntry<User> createdResult = await context.Set<User>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createdResult.Entity;
        }

        public async Task<bool> Delete(int id)
        {
                User? entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<User>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
                IEnumerable<User> entities = await context.Set<User>().Include(u => u.ProjectUsers).ToListAsync();

                return entities;
        }

        public async Task<User> Get(int id)
        {
                User? entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
        }


        public async Task<User> GetByEmail(string email)
        {
                User? entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.Email == email);
                return entity;
        }

        public async Task<User> Update(int id, User entity)
        {
                entity.Id = id;

                context.Set<User>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
        }

        public async Task<User> GetByFullName(string name)
        {
            User? entity = await context.Set<User>().FirstOrDefaultAsync((e) => (e.FirstName + " " + e.LastName) == name);
            return entity;
        }
    }
}

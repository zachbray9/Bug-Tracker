using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly BugTrackerDbContext context;

        public GenericDataService(BugTrackerDbContext context)
        {
            this.context = context;
        }

        public async Task<T> Create(T entity)
        {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createdResult.Entity;
        }

        public async Task<bool> Delete(int id)
        {
                T? entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();

                return entities;
        }

        public async Task<T> Get(int id)
        {
                T? entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
        }

        public async Task<T> Update(int id, T entity)
        {
                entity.Id= id;

                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
        }
    }
}

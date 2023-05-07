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
    public class TicketDataService : ITicketService
    {
        private readonly BugTrackerDbContextFactory ContextFactory;

        public TicketDataService(BugTrackerDbContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<Ticket> Create(Ticket entity)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                EntityEntry<Ticket> createdResult = await context.Set<Ticket>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                Ticket? entity = await context.Set<Ticket>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<Ticket>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Ticket> Get(int id)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                Ticket? entity = await context.Set<Ticket>().Include(t => t.Comments).FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                IEnumerable<Ticket> entities = await context.Set<Ticket>().Include(t => t.Comments).ToListAsync();

                return entities;
            }
        }

        public async Task<Ticket> Update(int id, Ticket entity)
        {
            using (BugTrackerDbContext context = ContextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<Ticket>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}

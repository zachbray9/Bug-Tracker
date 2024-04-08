using BugTracker.Domain.Models;
using BugTracker.Domain.Models.Auth;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BugTracker.Api.Services.SessionServices
{
    public class SessionDbService
    {
        private readonly BugTrackerDbContext DbContext;

        public SessionDbService(BugTrackerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<AgileSession> CreateSession(User user)
        {
            AgileSession session = new AgileSession
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ExpirationDate = DateTime.UtcNow.AddDays(30)
            };

            EntityEntry<AgileSession> newSession = await DbContext.Sessions.AddAsync(session);
            await DbContext.SaveChangesAsync();
            return newSession.Entity;
        }

        public async Task<bool> DeleteSession(string sessionId)
        {
            AgileSession? session = await DbContext.Sessions.FirstOrDefaultAsync(s => s.Id.ToString() == sessionId);
            if (session == null)
            {
                return false;
            }

            DbContext.Sessions.Remove(session);
            await DbContext.SaveChangesAsync();
            return true;
        }
    }
}

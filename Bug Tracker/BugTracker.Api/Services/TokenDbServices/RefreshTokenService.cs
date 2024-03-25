using BugTracker.Domain.Models.Auth;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BugTracker.Api.Services.TokenDbServices
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly BugTrackerDbContext DbContext;

        public RefreshTokenService(BugTrackerDbContext dbcontext)
        {
            DbContext = dbcontext;
        }

        public async Task Create(RefreshToken refreshToken)
        {
            EntityEntry entity =  DbContext.RefreshTokens.Add(refreshToken);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            RefreshToken? refreshToken = await DbContext.RefreshTokens.FindAsync(id);
            if (refreshToken != null)
            {
                DbContext.RefreshTokens.Remove(refreshToken);
                await DbContext.SaveChangesAsync();
            }

        }

        public async Task DeleteAll(int userId)
        {
            IEnumerable<RefreshToken> refreshTokens = await DbContext.RefreshTokens
                .Where(t => t.UserId == userId)
                .ToListAsync();

            DbContext.RefreshTokens.RemoveRange(refreshTokens);
            await DbContext.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByToken(string token)
        {
            return await DbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }
    }
}

using BugTracker.Domain.Models.Auth;

namespace BugTracker.Api.Services.TokenDbServices
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken?> GetByToken(string token);
        Task Create(RefreshToken refreshToken);
        Task Delete(Guid id);
        Task DeleteAll(int userId);
    }
}

using BugTracker.Domain.Models.DTOs;

namespace BugTracker.Domain.Services.Api
{
    public interface IProjectUserApiService : IApiService<ProjectUserDTO>
    {
        Task<ProjectUserDTO> GetByProjectAndUserId(int projectId, int userId);
    }
}

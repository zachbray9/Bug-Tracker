using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Database;

namespace BugTracker.Domain.Services.Api
{
    public interface IProjectUserApiService
    {
        Task<ProjectUserDTO> GetByProjectAndUserIdAsync(Guid projectId, string userId);
        Task<List<ProjectUserDTO>> GetAllAsync();
        Task<ProjectUserDTO> CreateAsync(Guid projectId, ProjectUserDTO projectUserDTO);
        Task<ProjectUserDTO> UpdateAsync(Guid projectId, string userId, ProjectUserDTO projectUserDTO);
        Task<bool> DeleteAsync (int projectId, string userId);
    }
}

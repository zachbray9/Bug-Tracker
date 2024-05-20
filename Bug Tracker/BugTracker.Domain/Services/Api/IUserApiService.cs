using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Database;

namespace BugTracker.Domain.Services.Api
{
    public interface IUserApiService : IWritable<UserDTO>
    {
        Task<UserDTO> GetByIdAsync(string id);
        Task<UserDTO> GetByEmailAsync(string email);
        Task<UserDTO> GetByFullNameAsync(string fullName);
        Task<List<UserDTO>> GetAllAsync();
        Task<List<ProjectDTO>> GetAllProjectsFromUserByIdAsync(string id);
        Task<UserDTO> UpdateAsync (string id,  UserDTO user);
        Task<bool> DeleteAsync (string id);
    }
}

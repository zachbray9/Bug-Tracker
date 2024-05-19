using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Database;

namespace BugTracker.Domain.Services.Api
{
    public interface IProjectApiService : IReadable<ProjectDTO>, IWritable<ProjectDTO>, IUpdatable<ProjectDTO>, IDeletable<ProjectDTO>
    {
        Task<List<ProjectUserDTO>> GetAllUsersOnProject(int projectId);
        Task<List<TicketDTO>> GetAllTicketsOnProject(int projectId);
    }
}

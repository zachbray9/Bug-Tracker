using BugTracker.Domain.Models.DTOs;

namespace BugTracker.Domain.Services.Api
{
    public interface IProjectApiService : IApiService<ProjectDTO>
    {
        Task<List<TicketDTO>> GetAllTicketsOnProject(int projectId);
    }
}

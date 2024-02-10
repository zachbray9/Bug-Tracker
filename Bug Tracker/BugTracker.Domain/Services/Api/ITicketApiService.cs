using BugTracker.Domain.Models.DTOs;

namespace BugTracker.Domain.Services.Api
{
    public interface ITicketApiService : IApiService<TicketDTO>
    {
        Task<List<CommentDTO>> GetAllCommentsOnTicket(int ticketId);
    }
}

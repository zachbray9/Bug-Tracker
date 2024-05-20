using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Database;

namespace BugTracker.Domain.Services.Api
{
    public interface ITicketApiService : IReadable<TicketDTO>, IWritable<TicketDTO>, IUpdatable<TicketDTO>, IDeletable<TicketDTO>
    {
        Task<List<CommentDTO>> GetAllCommentsOnTicketAsync(Guid ticketId);
    }
}

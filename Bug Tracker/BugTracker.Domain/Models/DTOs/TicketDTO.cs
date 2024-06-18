using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models.DTOs
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ProjectParticipant Author { get; set; } = null!;
        public ProjectParticipant? Assignee { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}

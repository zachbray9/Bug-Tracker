using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models.DTOs
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public string AuthorFirstName { get; set; } = null!;
        public string AuthorLastName { get; set; } = null!;
        public string? AssigneeId { get; set; }
        public string? AssigneeFirstName { get; set; } = string.Empty;
        public string? AssigneeLastName { get; set; } = string.Empty;
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}

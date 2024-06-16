using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public User Author { get; set; } = null!;
        public string? AssigneeId { get; set; }
        public User? Assignee { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime DateSubmitted { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        

    }
}

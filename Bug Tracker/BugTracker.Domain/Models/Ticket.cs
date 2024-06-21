using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public ProjectUser Author { get; set; } = null!;
        public string? AssigneeId { get; set; }
        public Guid? AssigneeProjectId { get; set; }
        public ProjectUser? Assignee { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime DateSubmitted { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models
{
    public class ProjectUser
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ProjectRole Role { get; set; }
        public ICollection<Ticket> AuthoredTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

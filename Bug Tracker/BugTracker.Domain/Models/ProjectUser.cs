using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models
{
    public class ProjectUser
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ProjectRole Role { get; set; }
    }
}

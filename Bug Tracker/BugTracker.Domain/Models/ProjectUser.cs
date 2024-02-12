using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models
{
    public class ProjectUser : DomainObject
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ProjectRole Role { get; set; }
    }
}

using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models
{
    public class ProjectUser
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ProjectRole Role { get; set; }
    }
}

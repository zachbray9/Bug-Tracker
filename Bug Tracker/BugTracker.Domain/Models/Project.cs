namespace BugTracker.Domain.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateStarted { get; set; }
        public ICollection<ProjectUser> Users { get; set; } = new List<ProjectUser>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}

namespace BugTracker.Domain.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateStarted { get; set; }
        public ICollection<ProjectUser> Users { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        public Project()
        {
            Users = new List<ProjectUser>();
            Tickets = new List<Ticket>();
        }
    }
}

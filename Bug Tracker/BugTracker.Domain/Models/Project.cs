namespace BugTracker.Domain.Models
{
    public class Project : DomainObject
    {
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

    //public class Project : DomainObject
    //{
    //    public string Name { get; set; } = null!;
    //    public string Description { get; set; } = null!;
    //    public virtual ICollection<ProjectUser> ProjectUsers { get; set; } = null!;
    //    public virtual ICollection<Ticket> Tickets { get; set; } = null!;
    //    public DateTime DateStarted { get; set; }

    //    public Project()
    //    {
    //        ProjectUsers= new List<ProjectUser>();
    //        Tickets= new List<Ticket>();
    //    }
    //}
}

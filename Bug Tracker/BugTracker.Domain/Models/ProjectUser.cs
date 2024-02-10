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

    //public  class ProjectUser : DomainObject
    //{
    //    public virtual Project Project { get; set; } = null!;
    //    public int ProjectId { get; set; }

    //    public virtual User User { get; set; } = null!; 
    //    public int UserId { get; set; }

    //    public virtual ICollection<Ticket> AuthoredTickets { get; set; } = null!;
    //    public virtual ICollection<Ticket> AssignedTickets { get; set; } = null!;
    //    public virtual ICollection<Comment> Comments { get; set; } = null!;
    //    public ProjectRole Role { get; set; }

    //    public ProjectUser()
    //    {
    //        AuthoredTickets = new List<Ticket>();
    //        AssignedTickets = new List<Ticket>();
    //        Comments = new List<Comment>();
    //    }

    //    public override string ToString()
    //    {
    //        return User.FullName;
    //    }
    //}
}

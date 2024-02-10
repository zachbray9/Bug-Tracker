 namespace BugTracker.Domain.Models
{
    public class User : DomainObject
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime DateJoined { get; set; }
        public ICollection<ProjectUser> Projects { get; set; }
        public ICollection<Ticket> AuthoredTickets { get; set; }
        public ICollection<Ticket> AssignedTickets { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public User()
        {
            Projects = new List<ProjectUser>();
            AuthoredTickets = new List<Ticket>();
            AssignedTickets = new List<Ticket>();
            Comments = new List<Comment>();
        }

    }

    //public class User : DomainObject
    //{
    //    public string Email { get; set; } = null!;
    //    public string FirstName { get; set; } = null!;
    //    public string LastName { get; set; } = null!;
    //    [NotMapped]
    //    public string FullName { get => $"{FirstName} {LastName}"; }
    //    [NotMapped]
    //    public string Initials { get => $"{FirstName?.FirstOrDefault()}{LastName?.FirstOrDefault()}".ToUpper(); }
    //    public string PasswordHash { get; set; } = null!;
    //    public virtual ICollection<ProjectUser> ProjectUsers { get; set; } = null!;
    //    public DateTime DateJoined { get; set; }

    //    public User()
    //    {
    //        ProjectUsers = new List<ProjectUser>();
    //    }

    //    public override string ToString()
    //    {
    //        return Email;
    //    }
    //}
}

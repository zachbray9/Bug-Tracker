using Microsoft.AspNetCore.Identity;

namespace BugTracker.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
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
}

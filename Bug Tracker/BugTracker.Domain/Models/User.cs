using Microsoft.AspNetCore.Identity;

namespace BugTracker.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public DateTime DateJoined { get; set; }
        public ICollection<ProjectUser> Projects { get; set; } = new List<ProjectUser>();
        public ICollection<Ticket> AuthoredTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

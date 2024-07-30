using Microsoft.AspNetCore.Identity;

namespace BugTracker.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public ICollection<ProjectUser> Projects { get; set; } = new List<ProjectUser>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();    
    }
}

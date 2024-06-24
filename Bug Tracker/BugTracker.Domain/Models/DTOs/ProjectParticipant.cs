using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models.DTOs
{
    public class ProjectParticipant
    {
        public string UserId { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public ProjectRole Role { get; set; }
    }
}

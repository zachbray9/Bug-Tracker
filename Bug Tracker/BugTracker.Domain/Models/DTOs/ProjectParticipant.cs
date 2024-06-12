namespace BugTracker.Domain.Models.DTOs
{
    public class ProjectParticipant
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public string Role { get; set; } = null!;
    }
}

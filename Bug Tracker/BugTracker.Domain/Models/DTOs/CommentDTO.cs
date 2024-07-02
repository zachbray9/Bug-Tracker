namespace BugTracker.Domain.Models.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public ProjectParticipant Author { get; set; } = null!;
        public DateTime DateSubmitted { get; set; }
        public string? TimeDifference { get; set; }
    }
}

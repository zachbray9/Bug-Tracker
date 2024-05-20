namespace BugTracker.Domain.Models.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public string AuthorFirstName { get; set; } = null!;
        public string AuthorLastName { get; set; } = null!;
        public string AuthorFullName { get => AuthorFirstName + " " + AuthorLastName; }
        public string AuthorInitials { get => $"{AuthorFirstName?.FirstOrDefault()}{AuthorLastName?.FirstOrDefault()}".ToUpper(); }
        public Guid TicketId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string? TimeDifference { get; set; }
    }
}

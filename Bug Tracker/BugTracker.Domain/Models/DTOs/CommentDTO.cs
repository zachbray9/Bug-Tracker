namespace BugTracker.Domain.Models.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int AuthorId { get; set; }
        public string AuthorFirstName { get; set; } = null!;
        public string AuthorLastName { get; set; } = null!;
        public int TicketId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string? TimeDifference { get; set; }
    }
}

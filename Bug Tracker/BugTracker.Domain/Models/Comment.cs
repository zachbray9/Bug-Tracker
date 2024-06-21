namespace BugTracker.Domain.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public ProjectUser Author { get; set; } = null!;
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}

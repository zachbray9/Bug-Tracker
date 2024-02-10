namespace BugTracker.Domain.Models
{
    public class Comment : DomainObject
    {
        public string Text { get; set; } = null!;
        public int AuthorId { get; set; }
        public User Author { get; set; } = null!;
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public DateTime DateSubmitted { get; set; }
    }

    //public class Comment : DomainObject
    //{
    //    public string Text { get; set; } = null!;

    //    public virtual ProjectUser Author { get; set; } = null!;
    //    public int AuthorId { get; set; }

    //    public virtual Ticket Ticket { get; set; } = null!;
    //    public int TicketId { get; set; }

    //    public DateTime DateSubmitted { get; set; }
    //    [NotMapped]
    //    public string? TimeDifference { get; set; }
    //}
}

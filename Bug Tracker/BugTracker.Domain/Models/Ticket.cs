using BugTracker.Domain.Enumerables;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public int AuthorId { get; set; }
        public User Author { get; set; } = null!;
        [NotMapped]
        public string AuthorFirstName { get => Author.FirstName; }
        [NotMapped]
        public string AuthorLastName { get => Author.LastName; }
        public int? AssigneeId { get; set; }
        public User? Assignee { get; set; }
        [NotMapped]
        public string AssigneeFirstName { get => Assignee != null ? Assignee.FirstName : string.Empty; }
        [NotMapped]
        public string AssigneeLastName { get => Assignee != null ? Assignee.LastName : string.Empty; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime DateSubmitted { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Ticket()
        {
            Comments = new List<Comment>();
        }

    }
}

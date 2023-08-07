using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models
{
    public class Comment : DomainObject
    {
        public string Text { get; set; } = null!;

        public virtual ProjectUser Author { get; set; } = null!;
        public int AuthorId { get; set; }

        public virtual Ticket Ticket { get; set; } = null!;
        public int TicketId { get; set; }

        public DateTime DateSubmitted { get; set; }
        [NotMapped]
        public string? TimeDifference { get; set; }
    }
}

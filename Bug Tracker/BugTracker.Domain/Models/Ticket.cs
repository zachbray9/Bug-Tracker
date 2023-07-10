using BugTracker.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models
{
    public class Ticket : DomainObject
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual Project Project { get; set; } = null!;
        public int ProjectId { get; set; }

        public virtual ProjectUser Author { get; set; } = null!;
        public int AuthorId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = null!;
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime DateSubmitted { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}

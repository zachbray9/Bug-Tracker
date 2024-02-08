using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Enumerables.EnumConverters;
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

        public virtual ProjectUser? Assignee { get; set; }
        public int AssigneeId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = null!;
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime DateSubmitted { get; set; }

        [NotMapped]
        private readonly StatusOptionsRetriever StatusOptionsRetriever;

        public Ticket()
        {
            StatusOptionsRetriever= new StatusOptionsRetriever();
        }

        [NotMapped]
        public string StatusString { get => StatusOptionsRetriever.ConvertStatusEnumToString(Status); }

        [NotMapped]
        public string AssigneeToolTipText
        {
            get
            {
                if (Assignee == null)
                    return "Unassigned";
                else
                    return $"Assignee: {Assignee.User.FullName}";
            }
        }

        public override string ToString()
        {
            return Title;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;

        [ForeignKey(nameof(AuthorId))]
        public ProjectUser Author { get; set; } = null!;
        public int AuthorId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public Ticket Ticket { get; set; } = null!;
        public int TicketId { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}

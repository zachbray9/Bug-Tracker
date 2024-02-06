using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int AuthorId { get; set; }
        public int TicketId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string? TimeDifference { get; set; }
    }
}

using BugTracker.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models.DTOs
{
    public class ProjectUserDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        public ICollection<TicketDTO> AuthoredTickets { get; set; } = null!;
        public ICollection<TicketDTO> AssignedTickets { get; set; } = null!;
        public ICollection<CommentDTO> Comments { get; set; } = null!;
        public ProjectRole Role { get; set; }

        public ProjectUserDTO()
        {
            AuthoredTickets = new List<TicketDTO>();
            AssignedTickets = new List<TicketDTO>();
            Comments = new List<CommentDTO>();
        }
    }
}

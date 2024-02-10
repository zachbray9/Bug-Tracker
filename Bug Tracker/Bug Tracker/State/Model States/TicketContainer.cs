using BugTracker.Domain.Models.DTOs;
using System.Collections.Generic;

namespace Bug_Tracker.State.Model_States
{
    public class TicketContainer : ITicketContainer
    {
        public TicketDTO CurrentTicket { get; set; }
        public ProjectUserDTO Assignee { get; set; }
        public ProjectUserDTO Author { get; set; }
        public ICollection<CommentDTO> CurrentCommentsOnTicket { get; set; } = new List<CommentDTO>();
    }
}

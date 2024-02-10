using BugTracker.Domain.Models.DTOs;
using System.Collections.Generic;

namespace Bug_Tracker.State.Model_States
{
    public interface ITicketContainer
    {
        TicketDTO CurrentTicket { get; set; }
        ProjectUserDTO Assignee {  get; set; }
        ProjectUserDTO Author { get; set; }
        ICollection<CommentDTO> CurrentCommentsOnTicket { get; set; }
    }
}

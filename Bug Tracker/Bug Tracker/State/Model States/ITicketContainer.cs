using BugTracker.Domain.Models.DTOs;

namespace Bug_Tracker.State.Model_States
{
    public interface ITicketContainer
    {
        TicketDTO CurrentTicket { get; set; }
        ProjectUserDTO Assignee {  get; set; }
        ProjectUserDTO Author { get; set; }
    }
}

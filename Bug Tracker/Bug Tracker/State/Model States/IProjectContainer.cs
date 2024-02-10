using BugTracker.Domain.Models.DTOs;
using System.Collections.Generic;

namespace Bug_Tracker.State
{
    public interface IProjectContainer
    {
        ProjectDTO CurrentProject { get; set; }
        ICollection<ProjectDTO> CurrentUserProjects { get; set; }
        ICollection<ProjectUserDTO> CurrentProjectUsers { get; set; }
        ICollection<TicketDTO> CurrentTicketsOnProject { get; set; }
    }
}

using BugTracker.Domain.Models.DTOs;
using System.Collections.Generic;

namespace Bug_Tracker.State.Model_States
{
    public class ProjectContainer : IProjectContainer
    {
        public ProjectDTO CurrentProject { get; set; }
        public ICollection<ProjectDTO> CurrentUserProjects { get; set; } = new List<ProjectDTO>();
        public ICollection<ProjectUserDTO> CurrentProjectUsers { get; set; } = new List<ProjectUserDTO>();
        public ICollection<TicketDTO> CurrentTicketsOnProject { get; set; } = new List<TicketDTO>();
    }
}

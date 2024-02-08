using BugTracker.Domain.Models.DTOs;

namespace Bug_Tracker.State.Model_States
{
    public class ProjectContainer : IProjectContainer
    {
        public ProjectDTO CurrentProject { get; set; }
    }
}

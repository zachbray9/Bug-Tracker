using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;

namespace Bug_Tracker.State
{
    public interface IProjectContainer
    {
        ProjectDTO CurrentProject { get; set; }
    }
}

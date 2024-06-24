using BugTracker.Domain.Models.DTOs;

namespace BugTracker.Api.Models.Requests
{
    public class UpdateTicketRequest
    {
        public string Id { get; set; } = null!;
        public string ProjectId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public ProjectParticipant Author { get; set; } = null!;
        public ProjectParticipant? Assignee { get; set; }
    }
}

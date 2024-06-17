using System.ComponentModel.DataAnnotations;

namespace BugTracker.Api.Models.Requests
{
    public class CreateTicketRequest
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Status { get; set; } = null!;
        [Required]
        public string Priority { get; set; } = null!;
        [Required]
        public string ProjectId { get; set; } = null!;

    }
}

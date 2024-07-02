using System.ComponentModel.DataAnnotations;

namespace BugTracker.Api.Models.Requests
{
    public class CreateCommentRequest
    {
        [Required]
        public Guid TicketId { get; set; }
        [Required]
        public string Text { get; set; } = null!;
    }
}

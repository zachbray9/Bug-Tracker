using System.ComponentModel.DataAnnotations;

namespace BugTracker.Api.Models.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}

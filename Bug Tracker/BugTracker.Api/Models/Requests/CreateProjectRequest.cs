using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BugTracker.Api.Models.Requests
{
    public class CreateProjectRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}

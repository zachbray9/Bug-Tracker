using System.ComponentModel.DataAnnotations;
using BugTracker.Domain.Enumerables;

namespace BugTracker.Api.Models.Requests
{
    public class AddUserToProjectRequest
    {
        public Guid ProjectId { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
    }
}

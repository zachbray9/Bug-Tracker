using System.ComponentModel.DataAnnotations;

namespace BugTracker.Api.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password {  get; set; } = string.Empty;
        [Required]
        public string ConfirmPassword {  get; set; } = string.Empty;
    }
}

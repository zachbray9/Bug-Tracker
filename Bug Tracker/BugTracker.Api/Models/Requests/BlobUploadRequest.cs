using System.ComponentModel.DataAnnotations;

namespace BugTracker.Api.Models.Requests
{
    public class BlobUploadRequest
    {
        [Required]
        public string FilePath { get; set; } = null!;
        [Required]
        public string FileName { get; set; } = null!;
    }
}

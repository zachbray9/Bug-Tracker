namespace BugTracker.Domain.Models.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateStarted { get; set; }
    }
}

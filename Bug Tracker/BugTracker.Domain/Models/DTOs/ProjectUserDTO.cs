using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models.DTOs
{
    public class ProjectUserDTO
    {
        public string UserId { get; set; } = null!;
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserFullName { get =>  UserFirstName + " " + UserLastName; }
        public string UserInitials { get => $"{UserFirstName?.FirstOrDefault()}{UserLastName?.FirstOrDefault()}".ToUpper(); }
        public Guid ProjectId { get; set; }
        public ProjectRole Role { get; set; }

        public override string ToString()
        {
            return UserFullName;
        }
    }
}

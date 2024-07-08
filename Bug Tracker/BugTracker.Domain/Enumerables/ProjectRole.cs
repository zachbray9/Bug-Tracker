using System.ComponentModel.DataAnnotations;

namespace BugTracker.Domain.Enumerables
{
    public enum ProjectRole
    {
        [Display(Name = "Owner")]
        Owner,
        [Display(Name = "Administrator")]
        Administrator,
        [Display(Name = "Developer")]
        Developer
    }

    public static class ProjectRoleExtensions
    {
        public static ProjectRole ParseProjectRole(string role)
        {
            switch (role)
            {
                case "Owner":
                    return ProjectRole.Owner;
                case "Administrator":
                    return ProjectRole.Administrator;
                case "Developer":
                    return ProjectRole.Developer;
                default:
                    throw new ArgumentException($"Invalid role string: {role}");
            }
        }
    }
}

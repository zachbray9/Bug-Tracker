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
}

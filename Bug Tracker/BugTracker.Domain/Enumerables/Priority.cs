using System.ComponentModel.DataAnnotations;

namespace BugTracker.Domain.Enumerables
{
    public enum Priority
    {
        [Display(Name ="low")]
        Low,
        [Display(Name = "medium")]
        Medium,
        [Display(Name = "high")]
        High
    }

    public static class PriorityExtensions
    {
        public static Priority ParsePriority(string priority)
        {
            switch(priority)
            {
                case "low":
                    return Priority.Low;
                case "medium":
                    return Priority.Medium;
                case "high":
                    return Priority.High;
                default:
                    throw new ArgumentException($"Invalid priority string: {priority}");
            }
        }
    }
}

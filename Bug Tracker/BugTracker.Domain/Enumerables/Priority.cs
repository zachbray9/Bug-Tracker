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

        public static string ParsePriority(Priority priority)
        {
            switch (priority)
            {
                case Priority.Low:
                    return "low";
                case Priority.Medium:
                    return "medium";
                case Priority.High:
                    return "high";
                default:
                    throw new ArgumentException($"Invalid priority: {priority}");
            }
        }
    }
}

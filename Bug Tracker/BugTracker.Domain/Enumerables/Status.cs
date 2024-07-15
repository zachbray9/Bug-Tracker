using System.ComponentModel.DataAnnotations;

namespace BugTracker.Domain.Enumerables
{
    public enum Status
    {
        [Display(Name = "To do")]
        ToDo,
        [Display(Name = "In progress")]
        InProgress,
        Done
    }

    public static class StatusExtensions
    {
        public static Status ParseStatus(string status)
        {
            switch(status)
            {
                case "To do":
                    return Status.ToDo;
                case "In progress":
                    return Status.InProgress;
                case "Done":
                    return Status.Done;
                default:
                    throw new ArgumentException($"Invalid status string: {status}");
                        
            }
        }

        public static string ParseStatus(Status status)
        {
            switch(status)
            {
                case Status.ToDo:
                    return "To do";
                case Status.InProgress:
                    return "In progress";
                case Status.Done:
                    return "Done";
                default:
                    throw new ArgumentException($"Invalid status: {status}");

            }
        }
    }
}

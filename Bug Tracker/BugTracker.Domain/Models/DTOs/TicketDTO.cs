using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models.DTOs
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public string AuthorId { get; set; } = null!;
        public string AuthorFirstName { get; set; } = null!;
        public string AuthorLastName { get; set; } = null!;
        public string AuthorInitials { get => $"{AuthorFirstName?.FirstOrDefault()}{AuthorLastName?.FirstOrDefault()}".ToUpper(); }
        public string? AssigneeId { get; set; }
        public string? AssigneeFirstName { get; set; } = string.Empty;
        public string? AssigneeLastName { get; set; } = string.Empty;
        public string? AssigneeInitials { get => $"{AssigneeFirstName?.FirstOrDefault()}{AssigneeLastName?.FirstOrDefault()}".ToUpper(); }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime DateSubmitted { get; set; }

        public string AssigneeToolTipText
        {
            get
            {
                if (string.IsNullOrEmpty(AssigneeId))
                    return "Unassigned";
                else
                    return $"Assignee: {AssigneeFirstName + " " + AssigneeLastName}";
            }
        }
    }

    //public class TicketDTO
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; } = null!;
    //    public string Description { get; set; } = null!;
    //    public int ProjectId { get; set; }
    //    public int AuthorId { get; set; }
    //    public int AssigneeId { get; set; }
    //    public ICollection<CommentDTO> Comments { get; set; } = null!;
    //    public Status Status { get; set; }
    //    public Priority Priority { get; set; }
    //    public TicketType TicketType { get; set; }
    //    public DateTime DateSubmitted { get; set; }

    //    private readonly StatusOptionsRetriever StatusOptionsRetriever;

    //    public TicketDTO()
    //    {
    //        StatusOptionsRetriever = new StatusOptionsRetriever();
    //    }

    //    public string StatusString { get => StatusOptionsRetriever.ConvertStatusEnumToString(Status); }


    //    public override string ToString()
    //    {
    //        return Title;
    //    }
    //}
}

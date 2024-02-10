using BugTracker.Domain.Enumerables;

namespace BugTracker.Domain.Models.DTOs
{
    public class ProjectUserDTO
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public ProjectRole Role { get; set; }
    }

    //public class ProjectUserDTO
    //{
    //    public int Id { get; set; }
    //    public int ProjectId { get; set; }
    //    public int UserId { get; set; }

    //    public ICollection<TicketDTO> AuthoredTickets { get; set; } = null!;
    //    public ICollection<TicketDTO> AssignedTickets { get; set; } = null!;
    //    public ICollection<CommentDTO> Comments { get; set; } = null!;
    //    public ProjectRole Role { get; set; }

    //    public ProjectUserDTO()
    //    {
    //        AuthoredTickets = new List<TicketDTO>();
    //        AssignedTickets = new List<TicketDTO>();
    //        Comments = new List<CommentDTO>();
    //    }
    //}
}

using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models.DTOs
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
        public int AuthorId { get; set; }
        public int? AssigneeId { get; set; }

        public ICollection<CommentDTO> Comments { get; set; } = null!;
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime DateSubmitted { get; set; }

        private readonly StatusOptionsRetriever StatusOptionsRetriever;

        public TicketDTO()
        {
            StatusOptionsRetriever = new StatusOptionsRetriever();
        }

        public string StatusString { get => StatusOptionsRetriever.ConvertStatusEnumToString(Status); }

        //public string AssigneeToolTipText
        //{
        //    get
        //    {
        //        if (Assignee == null)
        //            return "Unassigned";
        //        else
        //            return $"Assignee: {Assignee.User.FullName}";
        //    }
        //}

        public override string ToString()
        {
            return Title;
        }
    }
}

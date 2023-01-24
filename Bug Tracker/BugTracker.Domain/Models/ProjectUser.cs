using BugTracker.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models
{
    public  class ProjectUser : DomainObject
    {
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; } = null!;
        public int ProjectId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!; 
        public int UserId { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = null!;
        public ProjectRole Role { get; set; }
    }
}

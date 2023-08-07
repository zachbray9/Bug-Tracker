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
        public virtual Project Project { get; set; } = null!;
        public int ProjectId { get; set; }

        public virtual User User { get; set; } = null!; 
        public int UserId { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = null!;
        public ProjectRole Role { get; set; }

        public ProjectUser()
        {
            Tickets = new List<Ticket>();
            Comments = new List<Comment>();
        }

        public override string ToString()
        {
            return User.FullName;
        }
    }
}

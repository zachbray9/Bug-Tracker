using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<ProjectUser> ProjectUsers { get; set; } = null!;
        public ICollection<Ticket> Tickets { get; set; } = null!;
        public DateTime DateStarted { get; set; }
    }
}

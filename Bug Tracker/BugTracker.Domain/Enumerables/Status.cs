using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Enumerables
{
    public enum Status
    {
        [Display(Name = "To Do")]
        ToDo,
        [Display(Name = "In Progress")]
        InProgress,
        Done
    }
}

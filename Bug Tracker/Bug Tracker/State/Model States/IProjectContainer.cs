using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.State
{
    public interface IProjectContainer
    {
        Project CurrentProject { get; set; }
        Ticket CurrentTicket { get; set; }
    }
}

using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.State.Model_States
{
    public class ProjectContainer : IProjectContainer
    {
        public Project CurrentProject { get; set; }
        public Ticket CurrentTicket { get; set; }
    }
}

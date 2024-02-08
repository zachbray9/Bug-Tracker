using BugTracker.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.State.Model_States
{
    public class TicketContainer : ITicketContainer
    {
        public TicketDTO CurrentTicket { get; set; }
        public ProjectUserDTO Assignee { get; set; }
        public ProjectUserDTO Author { get; set; }
    }
}

using Bug_Tracker.State;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class ViewTicketDetailsCommand : CommandBase
    {
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;

        public ViewTicketDetailsCommand(INavigator navigator, IProjectContainer projectContainer)
        {
            Navigator = navigator;
            ProjectContainer = projectContainer;
        }

        public override void Execute(object parameter)
        {
            Ticket ticket = (Ticket)parameter;
            if(ticket != null)
            {
                ProjectContainer.CurrentTicket = ticket;
                Navigator.Navigate(ViewType.TicketDetailsPage);
            }
        }
    }
}

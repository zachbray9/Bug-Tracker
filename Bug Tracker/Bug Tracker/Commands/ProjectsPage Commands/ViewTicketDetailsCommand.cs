using Bug_Tracker.State;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Models.DTOs;

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
            TicketDTO ticket = (TicketDTO)parameter;
            if(ticket != null)
            {
                ProjectContainer.CurrentProject = ticket.Project;
                ProjectContainer.CurrentTicket = ticket;
                Navigator.Navigate(ViewType.TicketDetailsPage);
            }
        }
    }
}

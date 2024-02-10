using Bug_Tracker.State;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models.DTOs;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class CreateNewTicketCommand : CommandBase
    {
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;
        private readonly ITicketContainer TicketContainer;

        private ProjectDTO CurrentProject { get => ProjectContainer.CurrentProject; }
        private TicketDTO CurrentTicket { get => TicketContainer.CurrentTicket; set { TicketContainer.CurrentTicket = value; } }

        public CreateNewTicketCommand(INavigator navigator, IProjectContainer projectContainer, ITicketContainer ticketContainer)
        {
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketContainer = ticketContainer;
        }

        public override void Execute(object parameter)
        {
            Status status = (Status)parameter;
            CurrentTicket = new TicketDTO 
            { 
                Title=string.Empty, 
                Description = string.Empty, 
                Status = status 
            };
            Navigator.Navigate(ViewType.CreateTicketPage);
        }
    }
}
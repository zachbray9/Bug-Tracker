using Bug_Tracker.State;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class CreateNewTicketCommand : CommandBase
    {
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;

        private ProjectDTO CurrentProject { get => ProjectContainer.CurrentProject; }
        private TicketDTO CurrentTicket { get => ProjectContainer.CurrentTicket; set { ProjectContainer.CurrentTicket = value; } }

        public CreateNewTicketCommand(INavigator navigator, IProjectContainer projectContainer)
        {
            Navigator = navigator;
            ProjectContainer = projectContainer;
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
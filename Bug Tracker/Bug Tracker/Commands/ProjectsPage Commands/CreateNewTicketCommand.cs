using Bug_Tracker.State;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class CreateNewTicketCommand : CommandBase
    {
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;

        private Project CurrentProject { get => ProjectContainer.CurrentProject; }
        private Ticket CurrentTicket { get => ProjectContainer.CurrentTicket; set { ProjectContainer.CurrentTicket = value; } }

        public CreateNewTicketCommand(INavigator navigator, IProjectContainer projectContainer)
        {
            Navigator = navigator;
            ProjectContainer = projectContainer;
        }

        public override void Execute(object parameter)
        {
            Status status = (Status)parameter;
            CurrentTicket = new Ticket 
            { 
                Title=string.Empty, 
                Description = string.Empty, 
                Status = status 
            };
            Navigator.Navigate(ViewType.CreateTicketPage);
        }
    }
}
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework;
using BugTracker.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class AddNewTicketToDbCommand : CommandBase
    {
        private readonly User CurrentUser;
        public INavigator Navigator{ get; }
        private readonly IProjectContainer ProjectContainer;
        private readonly IDataService<Ticket> TicketService;
        private readonly CreateTicketViewModel CreateTicketViewModel;

        private Project CurrentProject { get => ProjectContainer.CurrentProject; }
        private Ticket CurrentTicket { get => ProjectContainer.CurrentTicket; }

        public AddNewTicketToDbCommand(User currentUser, INavigator navigator, IProjectContainer projectContainer, IDataService<Ticket> ticketService, CreateTicketViewModel createTicketViewModel)
        {
            CurrentUser = currentUser;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketService = ticketService;
            CreateTicketViewModel = createTicketViewModel;
        }

        public async override void Execute(object parameter)
        {
            ProjectUser projectUser = CurrentUser.ProjectUsers.FirstOrDefault(pu => pu.User.Id == CurrentUser.Id);

            //this line is added to check if assignee is null or not to make it so the db doesn't throw an error when creating a ticket with a null assignee
            int? assigneeId = CreateTicketViewModel.Assignee?.Id;

            ProjectContainer.CurrentTicket = await TicketService.Create(new Ticket 
            { 
                Title = CreateTicketViewModel.TicketTitle, 
                Description = CreateTicketViewModel.TicketDescription, 
                ProjectId = CurrentProject.Id, 
                AuthorId = projectUser.Id, 
                AssigneeId = assigneeId, 
                Status = CurrentTicket.Status, 
                Priority = Priority.Low, 
                DateSubmitted = DateTime.Now 
            });
            Navigator.Navigate(ViewType.ProjectDetailsPage);
                           
        }
    }
}

using Bug_Tracker.State;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Linq;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class AddNewTicketToDbCommand : CommandBase
    {
        private readonly UserDTO CurrentUser;
        public INavigator Navigator{ get; }
        private readonly IProjectContainer ProjectContainer;
        private readonly ITicketContainer TicketContainer;
        private readonly ITicketApiService TicketApiService;
        private readonly CreateTicketViewModel CreateTicketViewModel;

        private ProjectDTO CurrentProject { get => ProjectContainer.CurrentProject; }
        private TicketDTO CurrentTicket { get => TicketContainer.CurrentTicket; }

        public AddNewTicketToDbCommand(UserDTO currentUser, INavigator navigator, IProjectContainer projectContainer, ITicketContainer ticketContainer, ITicketApiService ticketApiService, CreateTicketViewModel createTicketViewModel)
        {
            CurrentUser = currentUser;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketContainer = ticketContainer;
            TicketApiService = ticketApiService;
            CreateTicketViewModel = createTicketViewModel;
        }

        public async override void Execute(object parameter)
        {
            ProjectUserDTO projectUser = ProjectContainer.CurrentProjectUsers.FirstOrDefault(pu => pu.UserId == CurrentUser.Id);

            TicketContainer.CurrentTicket = await TicketApiService.CreateAsync(new TicketDTO 
            { 
                Title = CreateTicketViewModel.TicketTitle, 
                Description = CreateTicketViewModel.TicketDescription, 
                ProjectId = CurrentProject.Id, 
                AuthorId = CurrentUser.Id, 
                AuthorFirstName = CurrentUser.FirstName,
                AuthorLastName = CurrentUser.LastName,
                AssigneeId = CreateTicketViewModel.Assignee != null ? CreateTicketViewModel.Assignee.UserId : null, 
                AssigneeFirstName = CreateTicketViewModel.Assignee != null ? CreateTicketViewModel.Assignee.UserFirstName : null,
                AssigneeLastName = CreateTicketViewModel.Assignee != null ? CreateTicketViewModel.Assignee.UserLastName : null,
                Status = CurrentTicket.Status, 
                Priority = Priority.Low, 
                DateSubmitted = DateTime.Now 
            });

            ProjectContainer.CurrentTicketsOnProject.Add(TicketContainer.CurrentTicket);
            Navigator.Navigate(ViewType.ProjectDetailsPage);
                           
        }
    }
}

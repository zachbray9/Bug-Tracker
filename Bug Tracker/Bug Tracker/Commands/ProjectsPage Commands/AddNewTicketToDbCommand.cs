using Bug_Tracker.State;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services;
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
        private readonly IApiService<TicketDTO> TicketApiService;
        private readonly CreateTicketViewModel CreateTicketViewModel;

        private ProjectDTO CurrentProject { get => ProjectContainer.CurrentProject; }
        private TicketDTO CurrentTicket { get => ProjectContainer.CurrentTicket; }

        public AddNewTicketToDbCommand(UserDTO currentUser, INavigator navigator, IProjectContainer projectContainer, IApiService<TicketDTO> ticketApiService, CreateTicketViewModel createTicketViewModel)
        {
            CurrentUser = currentUser;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketApiService = ticketApiService;
            CreateTicketViewModel = createTicketViewModel;
        }

        public async override void Execute(object parameter)
        {
            ProjectUserDTO projectUser = CurrentUser.ProjectUsers.FirstOrDefault(pu => pu.UserId == CurrentUser.Id);

            //this line is added to check if assignee is null or not to make it so the db doesn't throw an error when creating a ticket with a null assignee
            int? assigneeId = CreateTicketViewModel.Assignee?.Id;

            ProjectContainer.CurrentTicket = await TicketApiService.Create(new TicketDTO 
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

using Bug_Tracker.State;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class ViewTicketDetailsCommand : CommandBase
    {
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;
        private readonly ITicketContainer TicketContainer;
        private readonly IProjectUserApiService ProjectUserApiService;
        private readonly IProjectApiService ProjectApiService;
        private readonly ITicketApiService TicketApiService;

        public ViewTicketDetailsCommand(INavigator navigator, IProjectContainer projectContainer, ITicketContainer ticketContainer, IProjectUserApiService projectUserDataService, IProjectApiService projectApiService, ITicketApiService ticketApiService)
        {
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketContainer = ticketContainer;
            ProjectUserApiService = projectUserDataService;
            ProjectApiService = projectApiService;
            TicketApiService = ticketApiService;
        }

        public override async void Execute(object parameter)
        {
            TicketDTO ticket = (TicketDTO)parameter;
            if(ticket != null)
            {
                ProjectContainer.CurrentProject = await ProjectApiService.GetById(ticket.ProjectId);
                TicketContainer.CurrentTicket = ticket;
                TicketContainer.CurrentCommentsOnTicket = await TicketApiService.GetAllCommentsOnTicket(ticket.Id);
                TicketContainer.Assignee = ticket.AssigneeId.HasValue ? await ProjectUserApiService.GetByProjectAndUserId(ticket.ProjectId, ticket.AssigneeId.Value) : null;
                TicketContainer.Author = await ProjectUserApiService.GetByProjectAndUserId(ticket.ProjectId, ticket.AuthorId);
                Navigator.Navigate(ViewType.TicketDetailsPage);
            }
        }
    }
}

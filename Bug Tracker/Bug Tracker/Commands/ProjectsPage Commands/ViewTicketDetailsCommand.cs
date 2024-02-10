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
        private readonly IApiService<ProjectUserDTO> ProjectUserApiService;
        private readonly IApiService<ProjectDTO> ProjectApiService;

        public ViewTicketDetailsCommand(INavigator navigator, IProjectContainer projectContainer, ITicketContainer ticketContainer, IApiService<ProjectUserDTO> projectUserDataService, IApiService<ProjectDTO> projectApiService)
        {
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketContainer = ticketContainer;
            ProjectUserApiService = projectUserDataService;
            ProjectApiService = projectApiService;
        }

        public override async void Execute(object parameter)
        {
            TicketDTO ticket = (TicketDTO)parameter;
            if(ticket != null)
            {
                ProjectContainer.CurrentProject = await ProjectApiService.GetById(ticket.ProjectId);
                TicketContainer.CurrentTicket = ticket;
                TicketContainer.Assignee = ticket.AssigneeId != 0 ? await ProjectUserApiService.GetById(ticket.AssigneeId) : null;
                TicketContainer.Author = await ProjectUserApiService.GetById(ticket.AuthorId);
                Navigator.Navigate(ViewType.TicketDetailsPage);
            }
        }
    }
}

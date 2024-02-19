using Bug_Tracker.Services.Api;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.TicketDetailsPageCommands
{
    public class CancelTicketDetailsChangesCommand : CommandBase
    {
        private readonly IProjectUserApiService ProjectUserApiService;
        private readonly TicketDetailsPageViewModel ViewModel;
        private readonly StatusOptionsRetriever StatusOptionsRetriever;
        private ProjectDTO CurrentProject { get => ViewModel.ProjectContainer.CurrentProject; }
        private TicketDTO CurrentTicket { get => ViewModel.TicketContainer.CurrentTicket; }

        public CancelTicketDetailsChangesCommand(IProjectUserApiService projectUserApiService, TicketDetailsPageViewModel viewModel, StatusOptionsRetriever statusOptionsRetriever)
        {
            ProjectUserApiService = projectUserApiService;
            ViewModel = viewModel;
            StatusOptionsRetriever = statusOptionsRetriever;
        }

        public async override void Execute(object parameter)
        {
            ViewModel.UserInputIsEnabled = false;

            ViewModel.TicketTitle = CurrentTicket.Title;
            ViewModel.TicketDescription = CurrentTicket.Description;
            ViewModel.Assignee = CurrentTicket.AssigneeId != null ? await ProjectUserApiService.GetByProjectAndUserId(CurrentProject.Id, CurrentTicket.AssigneeId.Value) : null;
            ViewModel.Reporter = await ProjectUserApiService.GetByProjectAndUserId(CurrentProject.Id, CurrentTicket.AuthorId);
            ViewModel.SetTicketStatusWithoutExecutingSaveCommand(StatusOptionsRetriever.ConvertStatusEnumToString(CurrentTicket.Status));

            ViewModel.UserInputIsEnabled = true;
        }
    }
}

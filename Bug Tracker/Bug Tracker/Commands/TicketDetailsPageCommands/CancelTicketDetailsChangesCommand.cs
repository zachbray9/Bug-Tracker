using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Models.DTOs;

namespace Bug_Tracker.Commands.TicketDetailsPageCommands
{
    public class CancelTicketDetailsChangesCommand : CommandBase
    {
        private readonly TicketDetailsPageViewModel ViewModel;
        private readonly StatusOptionsRetriever StatusOptionsRetriever;
        private TicketDTO CurrentTicket { get => ViewModel.CurrentTicket; }

        public CancelTicketDetailsChangesCommand(TicketDetailsPageViewModel viewModel, StatusOptionsRetriever statusOptionsRetriever)
        {
            ViewModel = viewModel;
            StatusOptionsRetriever = statusOptionsRetriever;
        }

        public override void Execute(object parameter)
        {
            ViewModel.TicketTitle = CurrentTicket.Title;
            ViewModel.TicketDescription = CurrentTicket.Description;
            ViewModel.SetAssigneeWithoutExecutingSaveCommand(CurrentTicket.Assignee);
            ViewModel.SetReporterWithoutExecutingSaveCommand(CurrentTicket.Author);
            ViewModel.SetTicketStatusWithoutExecutingSaveCommand(StatusOptionsRetriever.ConvertStatusEnumToString(CurrentTicket.Status));
        }
    }
}

using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bug_Tracker.Commands.TicketDetailsPageCommands
{
    public class SaveTicketDetailsChangesCommand : CommandBase
    {
        private readonly IDataService<Ticket> TicketDataService;
        private readonly TicketDetailsPageViewModel ViewModel;
        private readonly StatusOptionsRetriever StatusOptionsRetriever;

        private Ticket CurrentTicket { get => ViewModel.CurrentTicket; }

        public SaveTicketDetailsChangesCommand(IDataService<Ticket> ticketDataService, TicketDetailsPageViewModel viewModel, StatusOptionsRetriever statusOptionsRetriever)
        {
            TicketDataService = ticketDataService;
            ViewModel = viewModel;
            StatusOptionsRetriever = statusOptionsRetriever;
        }

        public async override void Execute(object parameter)
        {
            CurrentTicket.Title = ViewModel.TicketTitle;
            CurrentTicket.Description = ViewModel.TicketDescription;
            CurrentTicket.Assignee = ViewModel.Assignee;
            CurrentTicket.Author = ViewModel.Reporter;
            CurrentTicket.Status = StatusOptionsRetriever.ConvertStatusStringToEnum(ViewModel.TicketStatus);

            try
            {
                await TicketDataService.Update(CurrentTicket.Id, CurrentTicket);
                System.Diagnostics.Debug.WriteLine("Changes have been saved");

                ViewModel.TicketTitle = CurrentTicket.Title;
                ViewModel.TicketDescription = CurrentTicket.Description;
                ViewModel.SetAssigneeWithoutExecutingSaveCommand(CurrentTicket.Assignee);
                ViewModel.SetReporterWithoutExecutingSaveCommand(CurrentTicket.Author);
                ViewModel.SetTicketStatusWithoutExecutingSaveCommand(StatusOptionsRetriever.ConvertStatusEnumToString(CurrentTicket.Status));
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving your changes. Please try again.", "Save Changes Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

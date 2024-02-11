using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Windows;

namespace Bug_Tracker.Commands.TicketDetailsPageCommands
{
    public class SaveTicketDetailsChangesCommand : CommandBase
    {
        private readonly IApiService<TicketDTO> TicketApiService;
        private readonly TicketDetailsPageViewModel ViewModel;
        private readonly StatusOptionsRetriever StatusOptionsRetriever;

        private TicketDTO CurrentTicket { get => ViewModel.CurrentTicket; }

        public SaveTicketDetailsChangesCommand(IApiService<TicketDTO> ticketApiService, TicketDetailsPageViewModel viewModel, StatusOptionsRetriever statusOptionsRetriever)
        {
            TicketApiService = ticketApiService;
            ViewModel = viewModel;
            StatusOptionsRetriever = statusOptionsRetriever;
        }

        public async override void Execute(object parameter)
        {
            CurrentTicket.Title = ViewModel.TicketTitle;
            CurrentTicket.Description = ViewModel.TicketDescription;
            CurrentTicket.AssigneeId = ViewModel.Assignee != null ? ViewModel.Assignee.UserId : null;
            CurrentTicket.AuthorId = ViewModel.Reporter.UserId;
            CurrentTicket.Status = StatusOptionsRetriever.ConvertStatusStringToEnum(ViewModel.TicketStatus);

            try
            {
                await TicketApiService.Update(CurrentTicket.Id, CurrentTicket);
                System.Diagnostics.Debug.WriteLine("Changes have been saved");

                ViewModel.TicketTitle = CurrentTicket.Title;
                ViewModel.TicketDescription = CurrentTicket.Description;


                ViewModel.SetAssigneeWithoutExecutingSaveCommand();
                ViewModel.SetReporterWithoutExecutingSaveCommand();
                ViewModel.SetTicketStatusWithoutExecutingSaveCommand(StatusOptionsRetriever.ConvertStatusEnumToString(CurrentTicket.Status));
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving your changes. Please try again.", "Save Changes Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

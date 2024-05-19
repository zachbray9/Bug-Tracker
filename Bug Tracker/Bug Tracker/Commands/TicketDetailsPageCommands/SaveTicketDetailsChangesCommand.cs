using Bug_Tracker.State;
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
        private readonly IProjectApiService ProjectApiService;
        private readonly IProjectUserApiService ProjectUserApiService;
        private readonly ITicketApiService TicketApiService;
        private readonly IProjectContainer ProjectContainer;
        private readonly TicketDetailsPageViewModel ViewModel;
        private readonly StatusOptionsRetriever StatusOptionsRetriever;
        private bool TicketIsCurrentlyUpdating { get; set; }

        private TicketDTO CurrentTicket { get => ViewModel.CurrentTicket; }

        public SaveTicketDetailsChangesCommand(IProjectApiService projectApiService, IProjectUserApiService projectUserApiService, ITicketApiService ticketApiService, IProjectContainer projectContainer, TicketDetailsPageViewModel viewModel, StatusOptionsRetriever statusOptionsRetriever)
        {
            ProjectApiService = projectApiService;
            ProjectUserApiService = projectUserApiService;
            TicketApiService = ticketApiService;
            ProjectContainer = projectContainer;
            ViewModel = viewModel;
            StatusOptionsRetriever = statusOptionsRetriever;
        }

        public async override void Execute(object parameter)
        {
            if(TicketIsCurrentlyUpdating) 
                return;

            TicketIsCurrentlyUpdating = true;
            ViewModel.UserInputIsEnabled = false;
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

                ViewModel.Assignee = !string.IsNullOrEmpty(CurrentTicket.AssigneeId) ? await ProjectUserApiService.GetByProjectAndUserId(ProjectContainer.CurrentProject.Id, CurrentTicket.AssigneeId) : null;
                ViewModel.Reporter = await ProjectUserApiService.GetByProjectAndUserId(ProjectContainer.CurrentProject.Id, CurrentTicket.AuthorId);

                ViewModel.SetTicketStatusWithoutExecutingSaveCommand(StatusOptionsRetriever.ConvertStatusEnumToString(CurrentTicket.Status));
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving your changes. Please try again.", "Save Changes Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                TicketIsCurrentlyUpdating = false;
                ViewModel.UserInputIsEnabled = true;
            }
        }
    }
}

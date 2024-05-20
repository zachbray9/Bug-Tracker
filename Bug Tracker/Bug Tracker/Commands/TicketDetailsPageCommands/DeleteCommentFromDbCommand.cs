using Bug_Tracker.State.Model_States;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Bug_Tracker.Commands.TicketDetailsPageCommands
{
    public class DeleteCommentFromDbCommand : CommandBase
    {
        private readonly ICommentApiService CommentApiService;
        private readonly TicketDetailsPageViewModel ViewModel;
        private readonly ITicketContainer TicketContainer;
        private TicketDTO CurrentTicket { get => ViewModel.CurrentTicket; }

        public DeleteCommentFromDbCommand(ICommentApiService commentApiService, TicketDetailsPageViewModel viewModel, ITicketContainer ticketContainer)
        {
            CommentApiService = commentApiService;
            ViewModel = viewModel;
            TicketContainer = ticketContainer;
        }

        public override async void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Once you delete, it's gone for good.", "Delete this comment?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                CommentDTO commentToDelete = (CommentDTO)parameter;
                await CommentApiService.DeleteAsync(commentToDelete.Id);
                TicketContainer.CurrentCommentsOnTicket.Remove(commentToDelete);
                ViewModel.Comments = new ObservableCollection<CommentDTO>(TicketContainer.CurrentCommentsOnTicket.OrderByDescending(i => i.DateSubmitted));
            }
        }
    }
}

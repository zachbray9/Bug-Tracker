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
        private readonly IApiService<TicketDTO> TicketApiService;
        private readonly IApiService<CommentDTO> CommentApiService;
        private readonly TicketDetailsPageViewModel ViewModel;
        private TicketDTO CurrentTicket { get => ViewModel.CurrentTicket; }

        public DeleteCommentFromDbCommand(IApiService<TicketDTO> ticketApiService, IApiService<CommentDTO> commentApiService, TicketDetailsPageViewModel viewModel)
        {
            TicketApiService = ticketApiService;
            CommentApiService = commentApiService;
            ViewModel = viewModel;
        }

        public override async void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Once you delete, it's gone for good.", "Delete this comment?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                CommentDTO commentToDelete = (CommentDTO)parameter;
                await CommentApiService.DeleteById(commentToDelete.Id);
                ViewModel.Comments = new ObservableCollection<CommentDTO>(CurrentTicket.Comments.OrderByDescending(i => i.DateSubmitted));
            }
        }
    }
}

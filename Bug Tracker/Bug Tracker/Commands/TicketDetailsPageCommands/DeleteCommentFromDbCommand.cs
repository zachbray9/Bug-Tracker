using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bug_Tracker.Commands.TicketDetailsPageCommands
{
    public class DeleteCommentFromDbCommand : CommandBase
    {
        private readonly IDataService<Ticket> TicketDataService;
        private readonly IDataService<Comment> CommentDataService;
        private readonly TicketDetailsPageViewModel ViewModel;
        private Ticket CurrentTicket { get => ViewModel.CurrentTicket; }

        public DeleteCommentFromDbCommand(IDataService<Ticket> ticketDataService, IDataService<Comment> commentDataService, TicketDetailsPageViewModel viewModel)
        {
            TicketDataService = ticketDataService;
            CommentDataService = commentDataService;
            ViewModel = viewModel;
        }

        public override async void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Once you delete, it's gone for good.", "Delete this comment?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Comment commentToDelete = (Comment)parameter;
                await CommentDataService.Delete(commentToDelete.Id);
                ViewModel.Comments = new ObservableCollection<Comment>(CurrentTicket.Comments.OrderByDescending(i => i.DateSubmitted));
            }
        }
    }
}

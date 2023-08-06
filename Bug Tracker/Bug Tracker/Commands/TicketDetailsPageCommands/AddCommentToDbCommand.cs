using Bug_Tracker.State.Authenticators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.TicketDetailsPageCommands
{
    public class AddCommentToDbCommand : CommandBase
    {
        private readonly IAuthenticator Authenticator;
        private readonly IDataService<Ticket> TicketDataService;
        private readonly IDataService<Comment> CommentDataService;
        private readonly TicketDetailsPageViewModel ViewModel;

        private User CurrentUser { get => Authenticator.CurrentUser; }
        private Ticket CurrentTicket { get => ViewModel.CurrentTicket; }

        public AddCommentToDbCommand(IAuthenticator authenticator, IDataService<Ticket> ticketDataService, IDataService<Comment> commentDataService, TicketDetailsPageViewModel viewModel)
        {
            Authenticator = authenticator;
            TicketDataService = ticketDataService;
            CommentDataService = commentDataService;
            ViewModel = viewModel;
        }

        public async override void Execute(object parameter)
        {
            Comment newComment = new Comment
            {
                Text = ViewModel.CommentTextBoxText,
                AuthorId = CurrentUser.Id,
                TicketId = CurrentTicket.Id,
                DateSubmitted = DateTime.Now,
            };

            await CommentDataService.Create(newComment);
            CurrentTicket.Comments.Add(newComment);
            await TicketDataService.Update(CurrentTicket.Id, CurrentTicket);
            ViewModel.Comments = new ObservableCollection<Comment>(CurrentTicket.Comments);
            ViewModel.CommentTextBoxText = String.Empty;
        }
    }
}

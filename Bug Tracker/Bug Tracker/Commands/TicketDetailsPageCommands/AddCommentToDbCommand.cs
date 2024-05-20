using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bug_Tracker.Commands.TicketDetailsPageCommands
{
    public class AddCommentToDbCommand : CommandBase
    {
        private readonly IAuthenticator Authenticator;
        private readonly ICommentApiService CommentApiService;
        private readonly TicketDetailsPageViewModel ViewModel;
        private readonly IProjectContainer ProjectContainer;
        private readonly ITicketContainer TicketContainer;

        private UserDTO CurrentUser { get => Authenticator.CurrentUser; }
        private TicketDTO CurrentTicket { get => ViewModel.CurrentTicket; }

        public AddCommentToDbCommand(IAuthenticator authenticator, ICommentApiService commentApiService, TicketDetailsPageViewModel viewModel, IProjectContainer projectContainer, ITicketContainer ticketContainer)
        {
            Authenticator = authenticator;
            CommentApiService = commentApiService;
            ViewModel = viewModel;
            ProjectContainer = projectContainer;
            TicketContainer = ticketContainer;
        }

        public async override void Execute(object parameter)
        {
            CommentDTO newComment = new CommentDTO
            {
                Text = ViewModel.CommentTextBoxText,
                AuthorId = CurrentUser.Id,
                AuthorFirstName = CurrentUser.FirstName,
                AuthorLastName = CurrentUser.LastName,
                TicketId = CurrentTicket.Id,
                DateSubmitted = DateTime.Now,
            };

            newComment = await CommentApiService.CreateAsync(newComment);
            TicketContainer.CurrentCommentsOnTicket.Add(newComment);
            ViewModel.Comments = new ObservableCollection<CommentDTO>(TicketContainer.CurrentCommentsOnTicket.OrderByDescending(i => i.DateSubmitted));
            ViewModel.CommentTextBoxText = String.Empty;
        }
    }
}

using Bug_Tracker.State.Authenticators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models;
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
        private readonly IApiService<TicketDTO> TicketApiService;
        private readonly IApiService<CommentDTO> CommentApiService;
        private readonly TicketDetailsPageViewModel ViewModel;

        private UserDTO CurrentUser { get => Authenticator.CurrentUser; }
        private TicketDTO CurrentTicket { get => ViewModel.CurrentTicket; }

        public AddCommentToDbCommand(IAuthenticator authenticator, IApiService<TicketDTO> ticketApiService, IApiService<CommentDTO> commentApiService, TicketDetailsPageViewModel viewModel)
        {
            Authenticator = authenticator;
            TicketApiService = ticketApiService;
            CommentApiService = commentApiService;
            ViewModel = viewModel;
        }

        public async override void Execute(object parameter)
        {
            ProjectUserDTO projectUser = CurrentUser.ProjectUsers.FirstOrDefault(pu => pu.UserId == CurrentUser.Id);

            CommentDTO newComment = new CommentDTO
            {
                Text = ViewModel.CommentTextBoxText,
                AuthorId = projectUser.Id,
                TicketId = CurrentTicket.Id,
                DateSubmitted = DateTime.Now,
            };

            await CommentApiService.Create(newComment);
            CurrentTicket.Comments.Add(newComment);
            await TicketApiService.Update(CurrentTicket);
            ViewModel.Comments = new ObservableCollection<CommentDTO>(CurrentTicket.Comments.OrderByDescending(i => i.DateSubmitted));
            ViewModel.CommentTextBoxText = String.Empty;
        }
    }
}

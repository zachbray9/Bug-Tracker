using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Diagnostics;
using System.Windows;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class DeleteTicketCommand : CommandBase
    {
        private readonly ITicketApiService TicketApiService;
        private readonly IApiService<CommentDTO> CommentApiService;
        private readonly ProjectDetailsPageViewModel ViewModel;

        public DeleteTicketCommand(ITicketApiService ticketApiService, IApiService<CommentDTO> commentApiService, ProjectDetailsPageViewModel viewmodel)
        {
            TicketApiService = ticketApiService;
            CommentApiService = commentApiService;
            ViewModel = viewmodel;
        }

        public override async void Execute(object parameter)
        {
            TicketDTO ticketToDelete = (TicketDTO)parameter;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this task? You are about to permanently delete this ticket, its comments, and all of its data.", "Delete Task?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    //clearing all comments in ticket before deleting ticket because cascade delete doesn't want to work
                    //if(ticketToDelete.Comments != null)
                    //{
                    //    foreach(CommentDTO comment in ticketToDelete.Comments)
                    //    {
                    //        await CommentApiService.DeleteById(comment.Id);
                    //    }
                    //}

                    await TicketApiService.DeleteById(ticketToDelete.Id);
                    ViewModel.UpdateTickets();
                    ViewModel.UpdateFilteredTickets();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}

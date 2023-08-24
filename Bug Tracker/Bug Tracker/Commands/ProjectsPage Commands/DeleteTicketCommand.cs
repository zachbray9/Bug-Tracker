using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class DeleteTicketCommand : CommandBase
    {
        private readonly IDataService<Ticket> TicketDataService;
        private readonly IDataService<Comment> CommentDataService;
        private readonly ProjectDetailsPageViewModel ViewModel;

        public DeleteTicketCommand(IDataService<Ticket> ticketDataService, IDataService<Comment> commentDataService, ProjectDetailsPageViewModel viewmodel)
        {
            TicketDataService = ticketDataService;
            CommentDataService = commentDataService;
            ViewModel = viewmodel;
        }

        public override async void Execute(object parameter)
        {
            Ticket ticketToDelete = (Ticket)parameter;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this task? You are about to permanently delete this ticket, its comments, and all of its data.", "Delete Task?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    //clearing all comments in ticket before deleting ticket because cascade delete doesn't want to work
                    if(ticketToDelete.Comments != null)
                    {
                        foreach(Comment comment in ticketToDelete.Comments)
                        {
                            await CommentDataService.Delete(comment.Id);
                        }
                    }

                    await TicketDataService.Delete(ticketToDelete.Id);
                    ViewModel.UpdateTickets();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}

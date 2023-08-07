﻿using Bug_Tracker.Commands.TicketDetailsPageCommands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Bug_Tracker.ViewModels
{
    public class TicketDetailsPageViewModel : ViewModelBase, IDisposable
    {
        private readonly IAuthenticator Authenticator;
        public IProjectContainer ProjectContainer { get; }
        private readonly IDataService<Ticket> TicketDataService;
        private readonly IDataService<Comment> CommentDataService;

        public Ticket CurrentTicket => ProjectContainer.CurrentTicket;
        public bool DoesCommentTextBoxContainText { get => !CommentTextBoxText.IsNullOrEmpty(); }


        //DebounceTimer is so that changes are only saved to the database after a few seconds of inactivity so the thread isn't overloaded with db requests.
        private DispatcherTimer DebounceTimer;

        public TicketDetailsPageViewModel(IAuthenticator authenticator, IProjectContainer projectContainer, IDataService<Ticket> ticketDataService, IDataService<Comment> commentDataService, DispatcherTimer debounceTimer)
        {
            Authenticator = authenticator;
            ProjectContainer = projectContainer;
            TicketDataService = ticketDataService;
            CommentDataService = commentDataService;
            DebounceTimer = debounceTimer;

            DebounceTimer.Interval = TimeSpan.FromMilliseconds(500);
            DebounceTimer.IsEnabled = false;

            DebounceTimer.Tick += DebounceTimer_Tick;

            ticketTitle = CurrentTicket.Title;
            ticketDescription = CurrentTicket.Description;

            if(CurrentTicket.Comments != null) 
            {
                Comments = new ObservableCollection<Comment>(CurrentTicket.Comments.OrderByDescending(i => i.DateSubmitted));
            }
            else
            {
                Comments = new ObservableCollection<Comment>();
            }

            foreach (var comment in Comments)
            {
                comment.TimeDifference = CalculateTimeDifference(comment.DateSubmitted, DateTime.Now);
            }

            AddCommentToDbCommand = new AddCommentToDbCommand(Authenticator, TicketDataService, CommentDataService, this);
            DeleteCommentFromDbCommand = new DeleteCommentFromDbCommand(TicketDataService, CommentDataService, this);   
        }

        private string ticketTitle;
        public string TicketTitle
        {
            get { return ticketTitle; }
            set
            {
                ticketTitle = value;
                OnPropertyChanged(nameof(TicketTitle));
                StartDebounceTimer();   
            }
        }

        private string ticketDescription;
        public string TicketDescription
        {
            get { return ticketDescription; }
            set
            {
                ticketDescription = value;
                OnPropertyChanged(nameof(TicketDescription));
                StartDebounceTimer();
            }
        }

        private ObservableCollection<Comment> comments;
        public ObservableCollection<Comment> Comments 
        {
            get { return comments; }
            set
            {
                comments = value;

                //updates the CommentDateCreatedDifference every time the collection is set so it updates when a new comment is added or deleted.
                foreach (var comment in Comments)
                {
                    comment.TimeDifference = CalculateTimeDifference(comment.DateSubmitted, DateTime.Now);
                }
                OnPropertyChanged(nameof(Comments));
            }
        }

        private string commentDateCreatedDifference;
        public string CommentDateCreatedDifference
        {
            get { return commentDateCreatedDifference; }
            set 
            { 
                commentDateCreatedDifference = value;
                OnPropertyChanged(nameof(CommentDateCreatedDifference));
            }
        }

        private string commentTextBoxText;
        public string CommentTextBoxText
        {
            get { return commentTextBoxText; }
            set
            {
                commentTextBoxText = value;
                OnPropertyChanged(nameof(CommentTextBoxText));
                OnPropertyChanged(nameof(DoesCommentTextBoxContainText));
            }
        }


        private async Task SaveChangesAsync()
        {
            CurrentTicket.Title = TicketTitle;
            CurrentTicket.Description = TicketDescription;

            try
            {
                await TicketDataService.Update(CurrentTicket.Id, CurrentTicket);
                System.Diagnostics.Debug.WriteLine("Changes have been saved");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex}");
            }
        }

        private void StartDebounceTimer()
        {
            if(DebounceTimer.IsEnabled)
            {
                DebounceTimer.Stop();
            }

            DebounceTimer.Start();
            CommentDateCreatedDifference = CalculateTimeDifference(CurrentTicket.DateSubmitted, DateTime.Now);
        }

        private async void DebounceTimer_Tick(object sender, EventArgs e)
        {
            DebounceTimer.Stop();
            await SaveChangesAsync();
        }

        private string CalculateTimeDifference(DateTime DateCommentWasCreated, DateTime CurrentDate)
        {
            TimeSpan timeDifference = CurrentDate - DateCommentWasCreated;
            
            if(timeDifference.TotalMinutes < 1) 
            {
                return "Just now";
            }
            if(timeDifference.TotalMinutes < 60)
            {
                return $"{(int)timeDifference.TotalMinutes} minute{((int)timeDifference.TotalMinutes == 1 ? "" : "s")} ago";
            }
            else if (timeDifference.TotalHours < 24)
            {
                return $"{(int)timeDifference.TotalHours} hour{((int)timeDifference.TotalHours == 1 ? "" : "s")} ago";
            }
            else
            {
                return $"{(int)timeDifference.TotalDays} day{((int)timeDifference.TotalDays == 1 ? "" : "s")} ago";
            }
          
        }

        public void Dispose()
        {
            DebounceTimer.Tick -= DebounceTimer_Tick;
        }

        public ICommand AddCommentToDbCommand { get; }
        public ICommand DeleteCommentFromDbCommand { get; }
    }
}

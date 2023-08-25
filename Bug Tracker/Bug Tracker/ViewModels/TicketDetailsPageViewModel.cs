using Bug_Tracker.Commands.TicketDetailsPageCommands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Enumerables.EnumConverters;
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
        public INavigator Navigator { get; }
        public IProjectContainer ProjectContainer { get; }
        private readonly IDataService<Ticket> TicketDataService;
        private readonly IDataService<Comment> CommentDataService;

        private object saveLock = new object();
        private bool isSaveOperationInProgress = false;

        //DebounceTimer is so that changes are only saved to the database after a few seconds of inactivity so the thread isn't overloaded with db requests.
        private DispatcherTimer DebounceTimer;

        public StatusOptionsRetriever StatusOptionsRetriever { get; set; }

        public Project CurrentProject => ProjectContainer.CurrentProject;
        public Ticket CurrentTicket => ProjectContainer.CurrentTicket;
        public bool DoesCommentTextBoxContainText { get => !CommentTextBoxText.IsNullOrEmpty(); }



        public TicketDetailsPageViewModel(IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, IDataService<Ticket> ticketDataService, IDataService<Comment> commentDataService, DispatcherTimer debounceTimer, StatusOptionsRetriever statusOptionsRetriever)
        {
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketDataService = ticketDataService;
            CommentDataService = commentDataService;
            DebounceTimer = debounceTimer;
            StatusOptionsRetriever = statusOptionsRetriever;

            DebounceTimer.Interval = TimeSpan.FromMilliseconds(250);
            DebounceTimer.IsEnabled = false;

            DebounceTimer.Tick += DebounceTimer_Tick;

            ticketTitle = CurrentTicket.Title;
            ticketDescription = CurrentTicket.Description;
            ticketStatus = StatusOptionsRetriever.ConvertStatusEnumToString(CurrentTicket.Status);
            assignee = CurrentTicket.Assignee;
            reporter = CurrentTicket.Author;

            //checks if ticket has any comments and if so, fills the collection with tickets. Otherwise initializes empty collection
            if(CurrentTicket.Comments != null) 
            {
                Comments = new ObservableCollection<Comment>(CurrentTicket.Comments.OrderByDescending(i => i.DateSubmitted));
            }
            else
            {
                Comments = new ObservableCollection<Comment>();
            }

            //checks if the current project has any project users (which it always should) and if so, fills the collection with the project users. Otherwise initializes an empty list.
            if(CurrentProject.ProjectUsers != null) 
            {
                ProjectUsers = new ObservableCollection<ProjectUser>(CurrentProject.ProjectUsers);
            }
            else
            {
                ProjectUsers = new ObservableCollection<ProjectUser>();
            }

            //Calculates the time difference for each comment so that it can display how long ago it was posted.
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

        private string ticketStatus;
        public string TicketStatus
        {
            get { return ticketStatus; }
            set 
            {
                ticketStatus = value;
                OnPropertyChanged(nameof(TicketStatus));
                StartDebounceTimer();
            }
        }

        private ObservableCollection<ProjectUser> projectUsers;
        public ObservableCollection<ProjectUser> ProjectUsers
        {
            get { return projectUsers; }
            set
            {
                projectUsers = value;
                OnPropertyChanged(nameof(ProjectUsers));
            }
        }

        private ProjectUser assignee;
        public ProjectUser Assignee
        {
            get { return assignee; }
            set
            {
                assignee = value;
                OnPropertyChanged(nameof(Assignee));
                StartDebounceTimer();
            }
        }

        private ProjectUser reporter;
        public ProjectUser Reporter
        {
            get { return reporter; }
            set 
            {
                reporter = value;
                OnPropertyChanged(nameof(Reporter));
                StartDebounceTimer();
            }
        }

        private async Task SaveChangesAsync()
        {
            CurrentTicket.Title = TicketTitle;
            CurrentTicket.Description = TicketDescription;
            CurrentTicket.Assignee = Assignee;
            CurrentTicket.Author = Reporter;
            CurrentTicket.Status = StatusOptionsRetriever.ConvertStatusStringToEnum(TicketStatus);

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
            lock (saveLock)
            {
                if (isSaveOperationInProgress)
                {
                    return; // Another save operation is already in progress, skip this tick
                }

                isSaveOperationInProgress = true;
            }

            DebounceTimer.Stop();

            try
            {
                await SaveChangesAsync();
            }
            finally
            {
                lock (saveLock)
                {
                    isSaveOperationInProgress = false;
                }
            }
            
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

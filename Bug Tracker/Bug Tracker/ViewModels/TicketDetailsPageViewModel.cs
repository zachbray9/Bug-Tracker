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
    public class TicketDetailsPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; }
        public IProjectContainer ProjectContainer { get; }
        private readonly IDataService<Ticket> TicketDataService;
        private readonly IDataService<Comment> CommentDataService;

        public StatusOptionsRetriever StatusOptionsRetriever { get; set; }

        public User CurrentUser => Authenticator.CurrentUser;
        public Project CurrentProject => ProjectContainer.CurrentProject;
        public Ticket CurrentTicket => ProjectContainer.CurrentTicket;
        public ProjectUser CurrentProjectUser { get; }
        public bool DoesCommentTextBoxContainText { get => !CommentTextBoxText.IsNullOrEmpty(); }

        public TicketDetailsPageViewModel(IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, IDataService<Ticket> ticketDataService, IDataService<Comment> commentDataService, StatusOptionsRetriever statusOptionsRetriever)
        {
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketDataService = ticketDataService;
            CommentDataService = commentDataService;
            StatusOptionsRetriever = statusOptionsRetriever;

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

            CurrentProjectUser = ProjectUsers.FirstOrDefault(pu => pu.UserId == CurrentUser.Id);

            //Calculates the time difference for each comment so that it can display how long ago it was posted.
            foreach (var comment in Comments)
            {
                comment.TimeDifference = CalculateTimeDifference(comment.DateSubmitted, DateTime.Now);
            }

            AddCommentToDbCommand = new AddCommentToDbCommand(Authenticator, TicketDataService, CommentDataService, this);
            DeleteCommentFromDbCommand = new DeleteCommentFromDbCommand(TicketDataService, CommentDataService, this);
            SaveTicketDetailsChangesCommand = new SaveTicketDetailsChangesCommand(TicketDataService, this, StatusOptionsRetriever);
            CancelTicketDetailsChangesCommand = new CancelTicketDetailsChangesCommand(this, StatusOptionsRetriever);
        }

        //Ticket Title Properties
        private string ticketTitle;
        public string TicketTitle
        {
            get { return ticketTitle; }
            set
            {
                ticketTitle = value;
                OnPropertyChanged(nameof(TicketTitle));
                OnPropertyChanged(nameof(IsTicketTitleTextboxBeingEdited));  
            }
        }
        public bool IsTicketTitleTextboxBeingEdited
        {
            get
            {
                if(TicketTitle != CurrentTicket.Title) 
                    return true;

                return false;
            }
        }

        //Ticket Description Properties
        private string ticketDescription;
        public string TicketDescription
        {
            get { return ticketDescription; }
            set
            {
                ticketDescription = value;
                OnPropertyChanged(nameof(TicketDescription));
                OnPropertyChanged(nameof(IsTicketDescriptionTextboxBeingEdited));
            }
        }
        public bool IsTicketDescriptionTextboxBeingEdited
        {
            get
            {
                if(TicketDescription != CurrentTicket.Description)
                    return true;

                return false;
            }
        }

        //comment collection properties
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
                SaveTicketDetailsChangesCommand.Execute(this);
            }
        }

        public void SetTicketStatusWithoutExecutingSaveCommand(string value)
        {
            ticketStatus = value;
            OnPropertyChanged(nameof(TicketStatus));
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
                SaveTicketDetailsChangesCommand.Execute(this);
            }
        }
        public void SetAssigneeWithoutExecutingSaveCommand(ProjectUser value)
        {
            assignee = value;
            OnPropertyChanged(nameof(Assignee));
        }

        private ProjectUser reporter;
        public ProjectUser Reporter
        {
            get { return reporter; }
            set 
            {
                reporter = value;
                OnPropertyChanged(nameof(Reporter));
                SaveTicketDetailsChangesCommand.Execute(this);
            }
        }

        public void SetReporterWithoutExecutingSaveCommand(ProjectUser value)
        {
            reporter = value;
            OnPropertyChanged(nameof(Reporter));
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

        public ICommand AddCommentToDbCommand { get; }
        public ICommand DeleteCommentFromDbCommand { get; }
        public ICommand SaveTicketDetailsChangesCommand { get; }
        public ICommand CancelTicketDetailsChangesCommand { get; }
    }
}

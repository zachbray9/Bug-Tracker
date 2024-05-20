using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.Commands.TicketDetailsPageCommands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class TicketDetailsPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; }
        public IProjectContainer ProjectContainer { get; }
        public ITicketContainer TicketContainer { get; }
        private readonly IProjectUserApiService ProjectUserApiService;
        private readonly ITicketApiService TicketApiService;
        private readonly IProjectApiService ProjectApiService;
        private readonly ICommentApiService CommentApiService;

        public StatusOptionsRetriever StatusOptionsRetriever { get; set; }

        public UserDTO CurrentUser => Authenticator.CurrentUser;
        public ProjectDTO CurrentProject => ProjectContainer.CurrentProject;
        public TicketDTO CurrentTicket => TicketContainer.CurrentTicket;
        public ProjectUserDTO CurrentProjectUser { get; }
        public bool DoesCommentTextBoxContainText { get => !String.IsNullOrEmpty(CommentTextBoxText); }

        public TicketDetailsPageViewModel(IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, ITicketContainer ticketContainer, IProjectUserApiService projectUserApiService, ITicketApiService ticketApiService, IProjectApiService projectApiService, ICommentApiService commentApiService, StatusOptionsRetriever statusOptionsRetriever)
        {
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketContainer = ticketContainer;
            ProjectUserApiService = projectUserApiService;
            TicketApiService = ticketApiService;
            ProjectApiService = projectApiService;
            CommentApiService = commentApiService;
            StatusOptionsRetriever = statusOptionsRetriever;

            ticketTitle = CurrentTicket.Title;
            ticketDescription = CurrentTicket.Description;
            ticketStatus = StatusOptionsRetriever.ConvertStatusEnumToString(CurrentTicket.Status);


            //checks if ticket has any comments and if so, fills the collection with tickets. Otherwise initializes empty collection
            if (TicketContainer.CurrentCommentsOnTicket != null) 
            {
                Comments = new ObservableCollection<CommentDTO>(TicketContainer.CurrentCommentsOnTicket.OrderByDescending(i => i.DateSubmitted));
            }
            else
            {
                Comments = new ObservableCollection<CommentDTO>();
            }

            //checks if the current project has any project users (which it always should) and if so, fills the collection with the project users. Otherwise initializes an empty list.
            if(ProjectContainer.CurrentProjectUsers != null) 
            {
                ProjectUsers = new ObservableCollection<ProjectUserDTO>(ProjectContainer.CurrentProjectUsers);
            }
            else
            {
                ProjectUsers = new ObservableCollection<ProjectUserDTO>();
            }

            Assignee = TicketContainer.Assignee != null ? ProjectUsers.FirstOrDefault(pu => pu.UserId == TicketContainer.CurrentTicket.AssigneeId) : null;
            Reporter = ProjectUsers.FirstOrDefault(pu => pu.UserId == TicketContainer.CurrentTicket.AuthorId);

            CurrentProjectUser = ProjectUsers.FirstOrDefault(pu => pu.UserId == CurrentUser.Id);
            UserInputIsEnabled = true;

            //Calculates the time difference for each comment so that it can display how long ago it was posted.
            foreach (var comment in Comments)
            {
                comment.TimeDifference = CalculateTimeDifference(comment.DateSubmitted, DateTime.Now);
            }

            AddCommentToDbCommand = new AddCommentToDbCommand(Authenticator, CommentApiService, this, ProjectContainer, TicketContainer);
            DeleteCommentFromDbCommand = new DeleteCommentFromDbCommand(CommentApiService, this, TicketContainer);
            SaveTicketDetailsChangesCommand = new SaveTicketDetailsChangesCommand(ProjectApiService, ProjectUserApiService, TicketApiService, ProjectContainer, this, StatusOptionsRetriever);
            CancelTicketDetailsChangesCommand = new CancelTicketDetailsChangesCommand(ProjectUserApiService, this, StatusOptionsRetriever);
            ViewProjectDetailsCommand = new ViewProjectDetailsCommand(Navigator, ProjectContainer, ProjectApiService);
        }

        private bool userInputIsEnabled;
        public bool UserInputIsEnabled
        {
            get => userInputIsEnabled;
            set
            {
                userInputIsEnabled = value;
                OnPropertyChanged(nameof(UserInputIsEnabled));
            }
        }

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

        public bool IsAssigneeComboBoxBeingEdited
        {
            get
            {
                if(Assignee == null)
                {
                    if(CurrentTicket.AssigneeId != null)
                        return true;
                }
                else
                {
                    if(Assignee.UserId != CurrentTicket.AssigneeId) 
                        return true;
                }
                
                return false;
            }
        }

        public bool IsReporterComboBoxBeingEdited
        {
            get
            {
                if (Reporter.UserId != CurrentTicket.AuthorId)
                    return true;

                return false;
            }
        }

        private ObservableCollection<CommentDTO> comments;
        public ObservableCollection<CommentDTO> Comments 
        {
            get { return comments; }
            set
            {
                comments = value;

                //updates the CommentDateCreatedDifference every time the collection is set so it updates when a new comment is added or deleted.
                foreach (CommentDTO comment in comments)
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

        private ObservableCollection<ProjectUserDTO> projectUsers;
        public ObservableCollection<ProjectUserDTO> ProjectUsers
        {
            get { return projectUsers; }
            set
            {
                projectUsers = value;
                OnPropertyChanged(nameof(ProjectUsers));
            }
        }

        private ProjectUserDTO assignee;
        public ProjectUserDTO Assignee
        {
            get { return assignee; }
            set
            {
                assignee = value;
                OnPropertyChanged(nameof(Assignee));
                OnPropertyChanged(nameof(IsAssigneeComboBoxBeingEdited));
            }
        }

        private ProjectUserDTO reporter;
        public ProjectUserDTO Reporter
        {
            get { return reporter; }
            set 
            {
                reporter = value;
                OnPropertyChanged(nameof(Reporter));
                OnPropertyChanged(nameof(IsReporterComboBoxBeingEdited));
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

        public ICommand AddCommentToDbCommand { get; }
        public ICommand DeleteCommentFromDbCommand { get; }
        public ICommand SaveTicketDetailsChangesCommand { get; }
        public ICommand CancelTicketDetailsChangesCommand { get; }
        public ICommand ViewProjectDetailsCommand { get; }
    }
}

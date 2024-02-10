using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels.Factories;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class ProjectDetailsPageViewModel : ViewModelBase
    {
        private readonly IUserApiService UserApiService;
        private readonly IApiService<ProjectUserDTO> ProjectUserApiService;
        private readonly IApiService<ProjectDTO> ProjectApiService;
        private readonly ITicketApiService TicketApiService;
        private readonly IApiService<CommentDTO> CommentApiService;
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; }
        public IProjectContainer ProjectContainer { get; }
        public ITicketContainer TicketContainer { get; }
        public AddUserToProjectPopupViewModel AddUserViewModel { get; }

        public UserDTO CurrentUser { get => Authenticator.CurrentUser; }
        public ProjectDTO CurrentProject { get => ProjectContainer.CurrentProject; }
        public ProjectUserDTO CurrentProjectUser { get; }

        private ObservableCollection<ProjectUserDTO> projectUsers;
        public ObservableCollection<ProjectUserDTO> ProjectUsers
        {
            get
            {
                return projectUsers;
            }

            set
            {
                projectUsers = value;
                OnPropertyChanged(nameof(ProjectUsers));

            }
        }

        private string ticketFilterQuery = String.Empty;
        public string TicketFilterQuery
        {
            get { return ticketFilterQuery; }
            set
            {
                ticketFilterQuery = value;
                OnPropertyChanged(nameof(TicketFilterQuery));
                UpdateFilteredTickets();
            }
        }

        //unfiltered Lists
        private ObservableCollection<TicketDTO> toDoTickets;
        public ObservableCollection<TicketDTO> ToDoTickets
        {
            get
            {
                return toDoTickets;
            }

            set
            {
                toDoTickets = value;
                OnPropertyChanged(nameof(ToDoTickets));

            }
        }

        private ObservableCollection<TicketDTO> inProgressTickets;
        public ObservableCollection<TicketDTO> InProgressTickets
        {
            get
            {
                return inProgressTickets;
            }

            set
            {
                inProgressTickets = value;
                OnPropertyChanged(nameof(InProgressTickets));

            }
        }

        private ObservableCollection<TicketDTO> doneTickets;
        public ObservableCollection<TicketDTO> DoneTickets
        {
            get
            {
                return doneTickets;
            }

            set
            {
                doneTickets = value;
                OnPropertyChanged(nameof(DoneTickets));

            }
        }

        //filtered lists
        private ObservableCollection<TicketDTO> filteredToDoTickets;
        public ObservableCollection<TicketDTO> FilteredToDoTickets
        {
            get
            {
                return filteredToDoTickets;
            }

            set
            {
                filteredToDoTickets = value;
                OnPropertyChanged(nameof(FilteredToDoTickets));

            }
        }

        private ObservableCollection<TicketDTO> filteredInProgressTickets;
        public ObservableCollection<TicketDTO> FilteredInProgressTickets
        {
            get
            {
                return filteredInProgressTickets;
            }

            set
            {
                filteredInProgressTickets = value;
                OnPropertyChanged(nameof(FilteredInProgressTickets));

            }
        }

        private ObservableCollection<TicketDTO> filteredDoneTickets;
        public ObservableCollection<TicketDTO> FilteredDoneTickets
        {
            get
            {
                return filteredDoneTickets;
            }

            set
            {
                filteredDoneTickets = value;
                OnPropertyChanged(nameof(FilteredDoneTickets));

            }
        }

        public ProjectDetailsPageViewModel(IUserApiService userApiService, IApiService<ProjectUserDTO> projectUserApiService, IApiService<ProjectDTO> projectApiService, ITicketApiService ticketApiService, IApiService<CommentDTO> commentApiService, IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, ITicketContainer ticketContainer, IViewModelAbstractFactory viewModelFactory)
        {
            UserApiService = userApiService;
            ProjectUserApiService = projectUserApiService;
            ProjectApiService = projectApiService;
            TicketApiService = ticketApiService;
            CommentApiService = commentApiService;
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketContainer = ticketContainer;
            AddUserViewModel = (AddUserToProjectPopupViewModel)viewModelFactory.CreateViewModel(ViewType.AddUserToProjectPopup);

            ProjectUsers = new ObservableCollection<ProjectUserDTO>();

            ToDoTickets = new ObservableCollection<TicketDTO>();
            InProgressTickets = new ObservableCollection<TicketDTO>();
            DoneTickets = new ObservableCollection<TicketDTO>();

            CreateNewTicketCommand = new CreateNewTicketCommand(Navigator, ProjectContainer, TicketContainer);
            ViewTicketDetailsCommand = new ViewTicketDetailsCommand(Navigator, ProjectContainer, TicketContainer, ProjectUserApiService, ProjectApiService);
            DeleteTicketCommand = new DeleteTicketCommand(TicketApiService, CommentApiService, this);
            OpenAddUserPopupCommand = new OpenAddUserPopupCommand(AddUserViewModel);

            UpdateProjectUsers();
            UpdateTickets();

            FilteredToDoTickets = new ObservableCollection<TicketDTO>(ToDoTickets);
            FilteredInProgressTickets = new ObservableCollection<TicketDTO>(InProgressTickets);
            FilteredDoneTickets = new ObservableCollection<TicketDTO>(DoneTickets);

            CurrentProjectUser = ProjectUsers.FirstOrDefault(pu => pu.UserId == CurrentUser.Id);
        }

        public void UpdateProjectUsers()
        {
            ProjectUsers.Clear();

            foreach(ProjectUserDTO projectUser in ProjectContainer.CurrentProjectUsers)
            {
                ProjectUsers.Add(projectUser);
            }               
        }

        public void UpdateTickets()
        {
            ToDoTickets.Clear();
            InProgressTickets.Clear();
            DoneTickets.Clear();

            foreach(TicketDTO ticket in ProjectContainer.CurrentTicketsOnProject)
            {
                if (ticket.Status == Status.ToDo)
                {
                    ToDoTickets.Add(ticket);
                    continue;
                }
                if (ticket.Status == Status.InProgress)
                {
                    InProgressTickets.Add(ticket);
                    continue;
                }
                if(ticket.Status == Status.Done)
                {
                    DoneTickets.Add(ticket);
                }
            }
        }

        public void UpdateFilteredTickets()
        {
            FilteredToDoTickets.Clear();
            FilteredInProgressTickets.Clear();
            FilteredDoneTickets.Clear();

            foreach (TicketDTO ticket in ToDoTickets)
            {
                if (ticket.Title.Contains(TicketFilterQuery, StringComparison.OrdinalIgnoreCase))
                {
                    FilteredToDoTickets.Add(ticket);
                }
            }

            foreach (TicketDTO ticket in InProgressTickets)
            {
                if (ticket.Title.Contains(TicketFilterQuery, StringComparison.OrdinalIgnoreCase))
                {
                    FilteredInProgressTickets.Add(ticket);
                }
            }

            foreach (TicketDTO ticket in DoneTickets)
            {
                if (ticket.Title.Contains(TicketFilterQuery, StringComparison.OrdinalIgnoreCase))
                {
                    FilteredDoneTickets.Add(ticket);
                }
            }
        }

        public ICommand CreateNewTicketCommand { get; }
        public ICommand ViewTicketDetailsCommand { get; }
        public ICommand DeleteTicketCommand { get; }
        public ICommand OpenAddUserPopupCommand { get; }
    }
}

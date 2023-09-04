using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels.Factories;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Bug_Tracker.ViewModels
{
    public class ProjectDetailsPageViewModel : ViewModelBase
    {
        private readonly IUserService UserDataService;
        private readonly IDataService<ProjectUser> ProjectUserService;
        private readonly IDataService<Ticket> TicketService;
        private readonly IDataService<Comment> CommentService;
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; }
        public IProjectContainer ProjectContainer { get; }
        public AddUserToProjectPopupViewModel AddUserViewModel { get; }

        public User CurrentUser { get => Authenticator.CurrentUser; }
        public Project CurrentProject { get => ProjectContainer.CurrentProject; }
        public ProjectUser CurrentProjectUser { get; }

        private ObservableCollection<ProjectUser> projectUsers;
        public ObservableCollection<ProjectUser> ProjectUsers
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
        private ObservableCollection<Ticket> toDoTickets;
        public ObservableCollection<Ticket> ToDoTickets
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

        private ObservableCollection<Ticket> inProgressTickets;
        public ObservableCollection<Ticket> InProgressTickets
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

        private ObservableCollection<Ticket> doneTickets;
        public ObservableCollection<Ticket> DoneTickets
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
        private ObservableCollection<Ticket> filteredToDoTickets;
        public ObservableCollection<Ticket> FilteredToDoTickets
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

        private ObservableCollection<Ticket> filteredInProgressTickets;
        public ObservableCollection<Ticket> FilteredInProgressTickets
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

        private ObservableCollection<Ticket> filteredDoneTickets;
        public ObservableCollection<Ticket> FilteredDoneTickets
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

        public ProjectDetailsPageViewModel(IUserService userDataService, IDataService<ProjectUser> projectUserService, IDataService<Ticket> ticketService, IDataService<Comment> commentService, IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, IViewModelAbstractFactory viewModelFactory)
        {
            UserDataService = userDataService;
            ProjectUserService = projectUserService;
            TicketService = ticketService;
            CommentService = commentService;
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            AddUserViewModel = (AddUserToProjectPopupViewModel)viewModelFactory.CreateViewModel(ViewType.AddUserToProjectPopup);

            ProjectUsers = new ObservableCollection<ProjectUser>();

            ToDoTickets = new ObservableCollection<Ticket>();
            InProgressTickets = new ObservableCollection<Ticket>();
            DoneTickets = new ObservableCollection<Ticket>();

            CreateNewTicketCommand = new CreateNewTicketCommand(Navigator, ProjectContainer);
            ViewTicketDetailsCommand = new ViewTicketDetailsCommand(Navigator, ProjectContainer);
            DeleteTicketCommand = new DeleteTicketCommand(TicketService, CommentService, this);
            OpenAddUserPopupCommand = new OpenAddUserPopupCommand(AddUserViewModel);

            UpdateProjectUsers();
            UpdateTickets();

            FilteredToDoTickets = new ObservableCollection<Ticket>(ToDoTickets);
            FilteredInProgressTickets = new ObservableCollection<Ticket>(InProgressTickets);
            FilteredDoneTickets = new ObservableCollection<Ticket>(DoneTickets);

            CurrentProjectUser = ProjectUsers.FirstOrDefault(pu => pu.UserId == CurrentUser.Id);
        }

        public void UpdateProjectUsers()
        {
            ProjectUsers.Clear();

            foreach(ProjectUser projectUser in CurrentProject.ProjectUsers)
            {
                ProjectUsers.Add(projectUser);
            }               
        }

        public void UpdateTickets()
        {
            ToDoTickets.Clear();
            InProgressTickets.Clear();
            DoneTickets.Clear();

            foreach(Ticket ticket in CurrentProject.Tickets)
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

            foreach (Ticket ticket in ToDoTickets)
            {
                if (ticket.Title.Contains(TicketFilterQuery, StringComparison.OrdinalIgnoreCase))
                {
                    FilteredToDoTickets.Add(ticket);
                }
            }

            foreach (Ticket ticket in InProgressTickets)
            {
                if (ticket.Title.Contains(TicketFilterQuery, StringComparison.OrdinalIgnoreCase))
                {
                    FilteredInProgressTickets.Add(ticket);
                }
            }

            foreach (Ticket ticket in DoneTickets)
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

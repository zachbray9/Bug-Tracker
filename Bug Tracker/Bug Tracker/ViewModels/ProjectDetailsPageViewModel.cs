using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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

        public User CurrentUser { get => Authenticator.CurrentUser; }
        public Project CurrentProject { get => ProjectContainer.CurrentProject; }

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

        public ProjectDetailsPageViewModel(IUserService userDataService, IDataService<ProjectUser> projectUserService, IDataService<Ticket> ticketService, IDataService<Comment> commentService, IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer)
        {
            UserDataService= userDataService;
            ProjectUserService= projectUserService;
            TicketService= ticketService;
            CommentService= commentService;
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;

            ProjectUsers = new ObservableCollection<ProjectUser>();
            ToDoTickets = new ObservableCollection<Ticket>();
            InProgressTickets = new ObservableCollection<Ticket>();
            DoneTickets = new ObservableCollection<Ticket>();

            CreateNewTicketCommand = new CreateNewTicketCommand(Navigator, ProjectContainer);
            ViewTicketDetailsCommand = new ViewTicketDetailsCommand(Navigator, ProjectContainer);
            DeleteTicketCommand = new DeleteTicketCommand(TicketService, CommentService, this);

            UpdateProjectUsers();
            UpdateTickets();
        }

        private async void UpdateProjectUsers()
        {
                foreach(ProjectUser projectUser in CurrentProject.ProjectUsers)
                {
                    projectUser.User = await UserDataService.Get(projectUser.UserId);
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
                    ToDoTickets.Add(ticket);
                else if (ticket.Status == Status.InProgress)
                    InProgressTickets.Add(ticket);
                else
                    DoneTickets.Add(ticket);
            }
        }

        public ICommand CreateNewTicketCommand { get; }
        public ICommand ViewTicketDetailsCommand { get; }
        public ICommand DeleteTicketCommand { get; }
    }
}

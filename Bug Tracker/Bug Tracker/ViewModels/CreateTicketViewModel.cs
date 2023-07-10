﻿using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class CreateTicketViewModel : ViewModelBase
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; }
        private readonly IProjectContainer ProjectContainer;
        private readonly IDataService<Ticket> TicketService;


        public ObservableCollection<string> StatusOptions { get; set; }
        public Dictionary<Status, string> StatusOptionsDictionary { get; set; }

        public User CurrentUser { get => Authenticator.CurrentUser; }


        public CreateTicketViewModel(BugTrackerDbContext dbContext, IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, IDataService<Ticket> ticketService)
        {
            DbContext = dbContext; 
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketService = ticketService;

            StatusOptionsDictionary = new Dictionary<Status, string>()
            {
                { Status.ToDo, "To Do" },
                { Status.InProgress, "In Progress"},
                { Status.Done, "Done"}
            };

            AddNewTicketToDbCommand = new AddNewTicketToDbCommand(CurrentUser, Navigator, ProjectContainer, TicketService, this);
        }

        private string ticketTitle;
        public string TicketTitle
        {
            get
            {
                return ticketTitle;
            }
            set
            {
                ticketTitle = value;
                OnPropertyChanged(nameof(TicketTitle));
            }
        }

        private string ticketDescription;
        public string TicketDescription
        {
            get
            {
                return ticketDescription;
            }
            set
            {
                ticketDescription = value;
                OnPropertyChanged(nameof(TicketDescription));
            }
        }

        private string ticketStatus;
        public string TicketStatus
        {
            get 
            {
                return ConvertStatusEnumToString(ProjectContainer.CurrentTicket.Status);
            }
            set
            {
                if(ticketStatus != value)
                {
                    ticketStatus = value;
                    ProjectContainer.CurrentTicket.Status = StatusOptionsDictionary.FirstOrDefault(x => x.Value == value).Key;
                    OnPropertyChanged(nameof(TicketStatus));
                }
            }
        }

        private string ConvertStatusEnumToString(Status status)
        {
            if(StatusOptionsDictionary.TryGetValue(status, out var value))
            {
                return value.ToString();
            }

            return String.Empty;
        }

        public ICommand AddNewTicketToDbCommand { get; }
    }
}

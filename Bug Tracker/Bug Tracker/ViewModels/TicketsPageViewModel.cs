using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class TicketsPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;
        private readonly ITicketContainer TicketContainer;
        private readonly IProjectUserApiService ProjectUserApiService;
        private readonly IProjectApiService ProjectApiService;
        private readonly ITicketApiService TicketApiService;

        private UserDTO CurrentUser { get => Authenticator.CurrentUser; }

        public TicketsPageViewModel(IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, ITicketContainer ticketContainer, IProjectUserApiService projectUserApiService, IProjectApiService projectApiService, ITicketApiService ticketApiService)
        {
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketContainer = ticketContainer;
            ProjectUserApiService = projectUserApiService;
            ProjectApiService = projectApiService;
            TicketApiService = ticketApiService;

            Tickets = new ObservableCollection<TicketDTO>();
            TicketSearchResults = new ObservableCollection<TicketDTO>();

            ViewTicketDetailsCommand = new ViewTicketDetailsCommand(Navigator, ProjectContainer, TicketContainer, ProjectUserApiService, ProjectApiService, TicketApiService);

            UpdateTickets();
            UpdateTicketSearchResults();
        }

        private string ticketFilterQuery = string.Empty;
        public string TicketFilterQuery
        {
            get { return ticketFilterQuery; }
            set
            {
                ticketFilterQuery = value;
                OnPropertyChanged(nameof(TicketFilterQuery));
                UpdateTicketSearchResults();
            }
        }

        private ObservableCollection<TicketDTO> tickets;
        public ObservableCollection<TicketDTO> Tickets
        {
            get { return tickets; }
            set
            {
                tickets = value;
                OnPropertyChanged(nameof(Tickets));
            }
        }

        private ObservableCollection<TicketDTO> ticketSearchResults;
        public ObservableCollection<TicketDTO> TicketSearchResults
        {
            get { return ticketSearchResults; }
            set
            {
                ticketSearchResults = value;
                OnPropertyChanged(nameof(TicketSearchResults));
            }
        }

        private void UpdateTickets()
        {
            Tickets.Clear();

            if (ProjectContainer.CurrentProjectUsers != null)
            {
                //Adds all authored and assigned tickets into a list (will contain duplicates if any ticket is authored and assigned by the current user)
                List<TicketDTO> allTicketsWithDuplicates = new List<TicketDTO>();
                foreach(ProjectUserDTO projectUser in ProjectContainer.CurrentProjectUsers)
                {
                    allTicketsWithDuplicates.AddRange(ProjectContainer.CurrentTicketsOnProject.Where(t => t.AuthorId == CurrentUser.Id));
                    allTicketsWithDuplicates.AddRange(ProjectContainer.CurrentTicketsOnProject.Where(t => t.AssigneeId == CurrentUser.Id));
                }

                //Creates a new list and checks if the new list already contains each ticket. If it doesn't contain the ticket, then the ticket is added.
                List<TicketDTO> allTicketsWithoutDuplicates = new List<TicketDTO>();
                foreach(TicketDTO ticket in allTicketsWithDuplicates)
                {
                    if(!allTicketsWithoutDuplicates.Contains(ticket))
                    {
                        allTicketsWithoutDuplicates.Add(ticket);
                    }
                }

                Tickets = new ObservableCollection<TicketDTO>(allTicketsWithoutDuplicates.OrderBy(ticket => ticket.Status));
            }
        }

        private void UpdateTicketSearchResults()
        {
            TicketSearchResults.Clear();

            foreach(TicketDTO ticket in Tickets)
            {
                if(ticket.Title.Contains(TicketFilterQuery, StringComparison.OrdinalIgnoreCase))
                {
                    TicketSearchResults.Add(ticket);
                }
            }
        }

        public ICommand ViewTicketDetailsCommand { get; }
    }
}

using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class TicketsPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;
        private readonly StatusOptionsRetriever StatusOptionsRetriever;

        private User CurrentUser { get => Authenticator.CurrentUser; }

        public TicketsPageViewModel(IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, StatusOptionsRetriever statusOptionsRetriever)
        {
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            StatusOptionsRetriever = statusOptionsRetriever;
            Tickets = new ObservableCollection<Ticket>();

            ViewTicketDetailsCommand = new ViewTicketDetailsCommand(Navigator, ProjectContainer);

            ResetTickets();
        }

        private ObservableCollection<Ticket> tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get { return tickets; }
            set
            {
                tickets = value;
                OnPropertyChanged(nameof(Tickets));
            }
        }

        //public string TicketStatus
        //{
        //    get { StatusOptionsRetriever.ConvertStatusEnumToString(); }
        //}

        private void ResetTickets()
        {
            Tickets.Clear();

            if (CurrentUser.ProjectUsers != null)
            {
                //Adds all authored and assigned tickets into a list (will contain duplicates if any ticket is authored and assigned by the current user)
                List<Ticket> allTicketsWithDuplicates = new List<Ticket>();
                foreach(ProjectUser projectUser in CurrentUser.ProjectUsers)
                {
                    allTicketsWithDuplicates.AddRange(projectUser.AuthoredTickets);
                    allTicketsWithDuplicates.AddRange(projectUser.AssignedTickets);
                }

                //Creates a new list and checks if the new list already contains each ticket. If it doesn't contain the ticket, then the ticket is added.
                List<Ticket> allTicketsWithoutDuplicates = new List<Ticket>();
                foreach(Ticket ticket in allTicketsWithDuplicates)
                {
                    if(!allTicketsWithoutDuplicates.Contains(ticket))
                    {
                        allTicketsWithoutDuplicates.Add(ticket);
                    }
                }

                Tickets = new ObservableCollection<Ticket>(allTicketsWithoutDuplicates);
            }
        }

        public ICommand ViewTicketDetailsCommand { get; }
    }
}

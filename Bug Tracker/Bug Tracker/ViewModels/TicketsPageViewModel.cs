using Bug_Tracker.State.Authenticators;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels
{
    public class TicketsPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        public ObservableCollection<Ticket> Tickets { get; set; }

        public TicketsPageViewModel(IAuthenticator authenticator)
        {
            Authenticator = authenticator;
            Tickets = new ObservableCollection<Ticket>();

            

            ResetTickets();
        }

        private void ResetTickets()
        {
            if (Authenticator.CurrentUser.ProjectUsers != null)
            {

                var tickets = from projectUser in Authenticator.CurrentUser.ProjectUsers
                              from Ticket in projectUser.Tickets
                              orderby Ticket?.DateSubmitted descending
                              select Ticket;

                Tickets = new ObservableCollection<Ticket>(tickets);
            }
        }

  
    }
}

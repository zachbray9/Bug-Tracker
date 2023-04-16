using Bug_Tracker.State.Authenticators;
using BugTracker.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels
{
    public class TicketsPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;

        public TicketsPageViewModel(IAuthenticator authenticator)
        {
            Authenticator = authenticator;
        }

  
    }
}

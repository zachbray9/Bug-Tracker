using Bug_Tracker.State.Authenticators;
using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;

        public User User 
        {
            get => Authenticator.CurrentUser;
        }

        public HomePageViewModel(IAuthenticator authenticator)
        {
            Authenticator = authenticator;
        }
    }
}

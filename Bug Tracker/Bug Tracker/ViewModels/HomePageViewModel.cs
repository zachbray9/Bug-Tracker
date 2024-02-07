using Bug_Tracker.State.Authenticators;
using BugTracker.Domain.Models.DTOs;

namespace Bug_Tracker.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;

        public UserDTO User 
        {
            get => Authenticator.CurrentUser;
        }

        public HomePageViewModel(IAuthenticator authenticator)
        {
            Authenticator = authenticator;
        }
    }
}

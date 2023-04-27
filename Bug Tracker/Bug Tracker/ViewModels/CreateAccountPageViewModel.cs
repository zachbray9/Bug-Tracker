using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels
{
    public class CreateAccountPageViewModel : ViewModelBase
    {
        public INavigator Navigator { get; }
        private readonly IAuthenticator Authenticator;
        public CreateAccountPageViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            Navigator = navigator;
            Authenticator = authenticator;
        }
    }
}

using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels.Factories
{
    public class LoginPageViewModelFactory : IViewModelFactory<LoginPageViewModel>
    {
        private readonly IAuthenticator Authenticator;

        public LoginPageViewModelFactory(IAuthenticator authenticator)
        {
            Authenticator = authenticator;
        }

        public LoginPageViewModel CreateViewModel()
        {
            return new LoginPageViewModel(Authenticator);
        }
    }
}

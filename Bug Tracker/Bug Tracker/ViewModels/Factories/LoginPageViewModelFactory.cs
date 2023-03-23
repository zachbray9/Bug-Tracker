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
        private readonly IRenavigator Renavigator;

        public LoginPageViewModelFactory(IAuthenticator authenticator, IRenavigator renavigator)
        {
            Authenticator = authenticator;
            Renavigator = renavigator;
        }

        public LoginPageViewModel CreateViewModel()
        {
            return new LoginPageViewModel(Authenticator, Renavigator);
        }
    }
}

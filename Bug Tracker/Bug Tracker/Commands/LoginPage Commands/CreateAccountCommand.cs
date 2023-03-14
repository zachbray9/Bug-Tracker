using Bug_Tracker.State.Authenticators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands
{
    public class CreateAccountCommand : CommandBase
    {
        private readonly LoginPageViewModel LoginPageViewModel;
        private readonly IAuthenticator Authenticator;

        public CreateAccountCommand(LoginPageViewModel loginPageViewModel, IAuthenticator authenticator)
        {
            LoginPageViewModel = loginPageViewModel;
            Authenticator = authenticator;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

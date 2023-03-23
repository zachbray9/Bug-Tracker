using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands
{
    public class AttemptLoginCommand : CommandBase
    {
        private readonly LoginPageViewModel LoginPageViewModel;
        private readonly IAuthenticator Authenticator;
        private readonly IRenavigator Renavigator;

        public AttemptLoginCommand(LoginPageViewModel loginPageViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            LoginPageViewModel = loginPageViewModel;
            Authenticator = authenticator;
            Renavigator = renavigator;
        }

        public async override void Execute(object parameter)
        {
            bool success = await Authenticator.Login(LoginPageViewModel.Username, parameter.ToString());
            if (success) 
            {
                Renavigator.Renavigate();
            }
        }
    }
}

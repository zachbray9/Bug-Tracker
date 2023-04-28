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
        private readonly INavigator Navigator;

        public AttemptLoginCommand(LoginPageViewModel loginPageViewModel, IAuthenticator authenticator, INavigator navigator)
        {
            LoginPageViewModel = loginPageViewModel;
            Authenticator = authenticator;
            Navigator= navigator;
        }

        public async override void Execute(object parameter)
        {
            bool success = await Authenticator.Login(LoginPageViewModel.Email, parameter.ToString());
            if (success) 
            {
                Navigator.Navigate(ViewType.HomePage);
            }
            else
            {
                LoginPageViewModel.LoginErrorText = "Your email and/or password is incorrect.";
            }
        }
    }
}

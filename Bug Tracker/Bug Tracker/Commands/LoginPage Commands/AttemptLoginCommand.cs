using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;

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
                Navigator.Navigate(ViewType.ProjectsPage);
            }
            else
            {
                LoginPageViewModel.LoginErrorText = "Your email and/or password is incorrect.";
            }
        }
    }
}

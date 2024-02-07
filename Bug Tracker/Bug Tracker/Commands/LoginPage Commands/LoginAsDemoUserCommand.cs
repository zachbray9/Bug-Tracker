using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;

namespace Bug_Tracker.Commands
{
    public class LoginAsDemoUserCommand : CommandBase
    {
        private readonly LoginPageViewModel LoginPageViewModel;
        private readonly IAuthenticator Authenticator;
        private readonly INavigator Navigator;

        public LoginAsDemoUserCommand(LoginPageViewModel loginPageViewModel, IAuthenticator authenticator, INavigator navigator)
        {
            LoginPageViewModel = loginPageViewModel;
            Authenticator = authenticator;
            Navigator = navigator;
        }

        public override async void Execute(object parameter)
        {
            bool success = await Authenticator.Login("test@gmail.com", "testPassword");
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

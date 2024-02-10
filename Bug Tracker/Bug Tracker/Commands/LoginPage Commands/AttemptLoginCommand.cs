using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Services.Api;
using System.Linq;

namespace Bug_Tracker.Commands
{
    public class AttemptLoginCommand : CommandBase
    {
        private readonly LoginPageViewModel LoginPageViewModel;
        private readonly IAuthenticator Authenticator;
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;
        private readonly IUserApiService UserApiService;

        public AttemptLoginCommand(LoginPageViewModel loginPageViewModel, IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, IUserApiService userApiService)
        {
            LoginPageViewModel = loginPageViewModel;
            Authenticator = authenticator;
            Navigator= navigator;
            ProjectContainer = projectContainer;
            UserApiService = userApiService;
        }

        public async override void Execute(object parameter)
        {
            bool success = await Authenticator.Login(LoginPageViewModel.Email, parameter.ToString());
            if (success) 
            {
                ProjectContainer.CurrentUserProjects = await UserApiService.GetAllProjectsFromUserById(Authenticator.CurrentUser.Id);
                Navigator.Navigate(ViewType.ProjectsPage);
            }
            else
            {
                LoginPageViewModel.LoginErrorText = "Your email and/or password is incorrect.";
            }
        }
    }
}

using Bug_Tracker.Commands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Services.Api;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; private set; }
        private readonly IProjectContainer ProjectContainer;
        private readonly IUserApiService UserApiService;

        public LoginPageViewModel(IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, IUserApiService userApiService)
        {
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            UserApiService = userApiService;

            AttemptLoginCommand = new AttemptLoginCommand(this, Authenticator, Navigator, ProjectContainer, UserApiService);
            LoginAsDemoUserCommand = new LoginAsDemoUserCommand(this, Authenticator, Navigator);
            //RecoverPasswordCommand = new RecoverPasswordCommand();

            loginErrorText = string.Empty;
            UserInputIsEnabled = true;
        }

        private string email;
        public string Email
        {
            get 
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string loginErrorText;
        public string LoginErrorText
        {
            get { return loginErrorText; }
            set
            {
                loginErrorText = value;
                OnPropertyChanged(nameof(LoginErrorText));
            }
        }

        private bool userInputIsEnabled;
        public bool UserInputIsEnabled
        {
            get => userInputIsEnabled;
            set
            {
                userInputIsEnabled = value;
                OnPropertyChanged(nameof(UserInputIsEnabled));
            }
        }

        public ICommand AttemptLoginCommand { get; }
        public ICommand LoginAsDemoUserCommand { get; }
        //public ICommand RecoverPasswordCommand { get; }
    }
}

using Bug_Tracker.Commands;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; private set; }

        public LoginPageViewModel(IAuthenticator authenticator, INavigator navigator)
        {
            Authenticator = authenticator;
            Navigator = navigator;

            AttemptLoginCommand = new AttemptLoginCommand(this, Authenticator, Navigator);
            LoginAsDemoUserCommand = new LoginAsDemoUserCommand(this, Authenticator, Navigator);
            //RecoverPasswordCommand = new RecoverPasswordCommand();

            loginErrorText = string.Empty;
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

        public ICommand AttemptLoginCommand { get; }
        public ICommand LoginAsDemoUserCommand { get; }
        //public ICommand RecoverPasswordCommand { get; }
    }
}

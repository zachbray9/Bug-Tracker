using Bug_Tracker.Commands;
using Bug_Tracker.Commands.Navigation_Commands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using BugTracker.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //RecoverPasswordCommand = new RecoverPasswordCommand();
            //CreateAccountCommand = new CreateAccountCommand(this, Authenticator);
            LoginAsDemoUserCommand = new LoginAsDemoUserCommand();

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
        //public ICommand RecoverPasswordCommand { get; }
        //public ICommand CreateAccountCommand { get; }
        public ICommand LoginAsDemoUserCommand { get; }
    }
}

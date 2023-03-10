using Bug_Tracker.Commands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
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

        public LoginPageViewModel()
        {
            AttemptLoginCommand = new AttemptLoginCommand(this, Authenticator);
            RecoverPasswordCommand = new RecoverPasswordCommand();
            CreateAccountCommand = new CreateAccountCommand(this, Authenticator);
            LoginAsDemoUserCommand = new LoginAsDemoUserCommand();
        }

        private string username;
        public string Username
        {
            get 
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
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

        public ICommand AttemptLoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand CreateAccountCommand { get; }
        public ICommand LoginAsDemoUserCommand { get; }
    }
}

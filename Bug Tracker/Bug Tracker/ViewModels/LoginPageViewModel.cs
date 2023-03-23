using Bug_Tracker.Commands;
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
        private readonly IRenavigator Renavigator;

        public LoginPageViewModel(IAuthenticator authenticator, IRenavigator renavigator)
        {
            Authenticator = authenticator;
            Renavigator = renavigator;

            AttemptLoginCommand = new AttemptLoginCommand(this, Authenticator, Renavigator);
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

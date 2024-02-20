using Bug_Tracker.Commands;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class CreateAccountPageViewModel : ViewModelBase
    {
        public INavigator Navigator { get; }
        private readonly IAuthenticator Authenticator;

        public CreateAccountPageViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            Navigator = navigator;
            Authenticator = authenticator;

            CreateAccountCommand = new CreateAccountCommand(this, Authenticator, Navigator);

            CreateAccountErrorText = string.Empty;
            UserInputIsEnabled = true;
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set 
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private string createAccountErrorText;
        public string CreateAccountErrorText
        {
            get { return createAccountErrorText; }
            set 
            {
                createAccountErrorText = value;
                OnPropertyChanged(nameof(CreateAccountErrorText));
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


        public ICommand CreateAccountCommand { get; }
    }
}

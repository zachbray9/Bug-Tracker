using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using System.Windows;

namespace Bug_Tracker.Commands
{
    public class CreateAccountCommand : CommandBase
    {
        private readonly CreateAccountPageViewModel CreateAccountPageViewModel;
        private readonly IAuthenticator Authenticator;
        private readonly INavigator Navigator;

        private string FirstName { get => CreateAccountPageViewModel.FirstName; }
        private string LastName { get => CreateAccountPageViewModel.LastName; }
        private string Email { get => CreateAccountPageViewModel.Email; }
        private string Password { get => CreateAccountPageViewModel.Password; }
        private string ConfirmPassword { get=> CreateAccountPageViewModel.ConfirmPassword; }


        public CreateAccountCommand(CreateAccountPageViewModel createAccountPageViewModel, IAuthenticator authenticator, INavigator navigator)
        {
            CreateAccountPageViewModel = createAccountPageViewModel;
            Authenticator = authenticator;
            Navigator = navigator;
        }

        public async override void Execute(object parameter)
        {
            CreateAccountPageViewModel.UserInputIsEnabled = false;

            RegistrationResult result = await Authenticator.CreateAccount(Email, FirstName, LastName, Password, ConfirmPassword);
            if(result == RegistrationResult.Success)
            {
                bool success = await Authenticator.Login(Email, Password);
                if(success)
                {
                    Navigator.Navigate(ViewType.ProjectsPage);
                }
                else
                {
                    MessageBox.Show("Something went wrong.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if(result == RegistrationResult.InputFieldIsNull)
            {
                CreateAccountPageViewModel.CreateAccountErrorText = "All input fields are required.";   
            }
            else if(result == RegistrationResult.NameContainsSpecialCharacter)
            {
                CreateAccountPageViewModel.CreateAccountErrorText = "Please remove any special characters from first or last name.";
            }
            else if(result == RegistrationResult.EmailFormatIsInvalid)
            {
                CreateAccountPageViewModel.CreateAccountErrorText = "The email format you entered is invalid. Please Enter a valid email.";
            }
            else if(result == RegistrationResult.EmailAlreadyExists)
            {
                CreateAccountPageViewModel.CreateAccountErrorText = "The email you are trying to use already exists.";
            }
            else
            {
                CreateAccountPageViewModel.CreateAccountErrorText = "The passwords do not match. Please try again.";
            }

            CreateAccountPageViewModel.UserInputIsEnabled = true;
        }

    }
}

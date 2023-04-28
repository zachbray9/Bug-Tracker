using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using BugTracker.EntityFramework.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            RegistrationResult result = await Authenticator.CreateAccount(Email, FirstName, LastName, Password, ConfirmPassword);
            if(result == RegistrationResult.Success)
            {
                bool success = await Authenticator.Login(Email, Password);
                if(success)
                {
                    Navigator.Navigate(ViewType.HomePage);
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
            else if(result == RegistrationResult.EmailAlreadyExists)
            {
                CreateAccountPageViewModel.CreateAccountErrorText = "The email you are trying to use already exists.";
            }
            else
            {
                CreateAccountPageViewModel.CreateAccountErrorText = "The passwords do not match. Please try again.";
            }
        }
    }
}

using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.Api;
using System;
using System.Net.Mail;
using System.Windows;

namespace Bug_Tracker.Commands.AccountCommands
{
    public class SaveAccountEditChangesCommand : CommandBase
    {
        private readonly IApiService<UserDTO> UserApiService;
        private readonly AccountPageViewModel ViewModel;

        private UserDTO CurrentUser { get => ViewModel.CurrentUser; }

        public SaveAccountEditChangesCommand(IApiService<UserDTO> userDataService, AccountPageViewModel viewModel)
        {
            UserApiService = userDataService;
            ViewModel = viewModel;
        }

        public async override void Execute(object parameter)
        {
            //check if any of the input boxes are null
            if (string.IsNullOrEmpty(ViewModel.FirstNameTextboxText) || string.IsNullOrEmpty(ViewModel.LastNameTextboxText) || string.IsNullOrEmpty(ViewModel.EmailTextboxText))
            {
                ViewModel.FirstNameTextboxText = CurrentUser.FirstName;
                ViewModel.LastNameTextboxText = CurrentUser.LastName;
                ViewModel.EmailTextboxText = CurrentUser.Email;
                MessageBox.Show("All fields are required. Please don't leave anything blank.", "Null Field Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //check if first name contains any specials characters
            if (!String.IsNullOrEmpty(ViewModel.FirstNameTextboxText))
            {
                foreach (char c in ViewModel.FirstNameTextboxText)
                {
                    if (!char.IsLetter(c))
                    {
                        ViewModel.FirstNameTextboxText = CurrentUser.FirstName;
                        MessageBox.Show("First name cannot contain any special characters.", "Invalid Character Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            //check if last name contains any special characters
            if (!String.IsNullOrEmpty(ViewModel.LastNameTextboxText))
            {
                foreach (char c in ViewModel.LastNameTextboxText)
                {
                    if (!char.IsLetter(c))
                    {
                        ViewModel.LastNameTextboxText = CurrentUser.LastName;
                        MessageBox.Show("Last name cannot contain any special characters.", "Invalid Character Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            //check if email is in a valid format
            if (!IsValidEmail(ViewModel.EmailTextboxText))
            {

                ViewModel.EmailTextboxText = CurrentUser.Email;
                MessageBox.Show("The email you entered is invalid. Please enter a valid email.", "Invalid Email Format Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //if all cases pass then save changes
            ViewModel.CurrentUser.FirstName = ViewModel.FirstNameTextboxText;
            CurrentUser.LastName = ViewModel.LastNameTextboxText;
            CurrentUser.Email = ViewModel.EmailTextboxText;

            try
            {
                await UserApiService.Update(CurrentUser);

                //doing this just so the save and cancel edit buttons disappear after saving changes.
                ViewModel.FirstNameTextboxText = CurrentUser.FirstName;
                ViewModel.LastNameTextboxText = CurrentUser.LastName;
                ViewModel.EmailTextboxText = CurrentUser.Email;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem saving your changes. Please try again.", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            bool isValid = true;

            try
            {
                MailAddress mailAddress = new MailAddress(email);
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }
    }
}

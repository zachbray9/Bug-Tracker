using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Net.Mail;
using System.Windows;

namespace Bug_Tracker.Commands.AccountCommands
{
    public class SaveAccountEditChangesCommand : CommandBase
    {
        private readonly IUserApiService UserApiService;
        private readonly AccountPageViewModel ViewModel;

        private UserDTO CurrentUser { get => ViewModel.CurrentUser; }

        public SaveAccountEditChangesCommand(IUserApiService userDataService, AccountPageViewModel viewModel)
        {
            UserApiService = userDataService;
            ViewModel = viewModel;
        }

        public async override void Execute(object parameter)
        {
            ViewModel.UserInputIsEnabled = false;

            //check if any of the input boxes are null
            if (string.IsNullOrEmpty(ViewModel.FirstNameTextboxText) || string.IsNullOrEmpty(ViewModel.LastNameTextboxText) || string.IsNullOrEmpty(ViewModel.EmailTextboxText))
            {
                CancelChanges();
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
                        CancelChanges();
                        MessageBox.Show("First name cannot contain any special characters.", "Invalid Character Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            //check if last name contains any special characters
            if (!string.IsNullOrEmpty(ViewModel.LastNameTextboxText))
            {
                foreach (char c in ViewModel.LastNameTextboxText)
                {
                    if (!char.IsLetter(c))
                    {
                        CancelChanges();
                        MessageBox.Show("Last name cannot contain any special characters.", "Invalid Character Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            //check if email is in a valid format
            if (!IsValidEmail(ViewModel.EmailTextboxText))
            {

                CancelChanges();
                MessageBox.Show("The email you entered is invalid. Please enter a valid email.", "Invalid Email Format Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //if all cases pass then save changes
            CurrentUser.FirstName = ViewModel.FirstNameTextboxText;
            CurrentUser.LastName = ViewModel.LastNameTextboxText;
            CurrentUser.Email = ViewModel.EmailTextboxText;

            try
            {
                await UserApiService.UpdateAsync(CurrentUser.Id, CurrentUser);
                CancelChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem saving your changes. Please try again.", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CancelChanges();
            }
            finally
            {
                ViewModel.UserInputIsEnabled = true;
            }
        }

        //helper methods

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

        private void CancelChanges()
        {
            ViewModel.FirstNameTextboxText = CurrentUser.FirstName;
            ViewModel.LastNameTextboxText = CurrentUser.LastName;
            ViewModel.EmailTextboxText = CurrentUser.Email;
            return;
        }
    }
}

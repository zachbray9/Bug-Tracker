using Bug_Tracker.Commands.AccountCommands;
using Bug_Tracker.State.Authenticators;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class AccountPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        private readonly IUserApiService UserApiService;

        public UserDTO CurrentUser { get => Authenticator.CurrentUser; }
        public bool IsDemoUser
        {
            get
            {
                if (string.Compare(CurrentUser.Email, "test@gmail.com", StringComparison.OrdinalIgnoreCase) == 0)
                    return true;

                return false;
            }
        }

        public AccountPageViewModel(IAuthenticator authenticator, IUserApiService userApiService)
        {
            Authenticator = authenticator;
            UserApiService = userApiService;      

            firstNameTextboxText = CurrentUser.FirstName;
            lastNameTextboxText = CurrentUser.LastName;
            emailTextboxText = CurrentUser.Email;

            CancelAccountEditCommand = new CancelAccountEditCommand(this);
            SaveAccountEditChangesCommand = new SaveAccountEditChangesCommand(UserApiService, this);
        }

        //first name properties
        private string firstNameTextboxText;
        public string FirstNameTextboxText
        {

            get { return firstNameTextboxText; }
            set
            {
                firstNameTextboxText = value;
                OnPropertyChanged(nameof(FirstNameTextboxText));
                OnPropertyChanged(nameof(IsFirstNameTextBoxBeingEdited));
            }
        }

        public bool IsFirstNameTextBoxBeingEdited
        {
            get
            {
                if (FirstNameTextboxText != CurrentUser.FirstName && !IsDemoUser)
                    return true;

                return false;  
            }
        }

        //last name properties
        private string lastNameTextboxText;
        public string LastNameTextboxText
        {

            get { return lastNameTextboxText; }
            set
            {
                lastNameTextboxText = value;
                OnPropertyChanged(nameof(LastNameTextboxText));
                OnPropertyChanged(nameof(IsLastNameTextBoxBeingEdited));
            }
        }

        public bool IsLastNameTextBoxBeingEdited
        {
            get
            {
                if (LastNameTextboxText != CurrentUser.LastName && !IsDemoUser)
                    return true;

                return false;
            }
        }

        //email properties
        private string emailTextboxText;
        public string EmailTextboxText
        {
            get { return emailTextboxText; }
            set
            {
                emailTextboxText = value;
                OnPropertyChanged(nameof(EmailTextboxText));
                OnPropertyChanged(nameof(IsEmailTextBoxBeingEdited));
            }
        }

        public bool IsEmailTextBoxBeingEdited
        {
            get
            {
                if (EmailTextboxText != CurrentUser.Email && !IsDemoUser)
                    return true;

                return false;
            }
        }
    
        public ICommand CancelAccountEditCommand { get; }
        public ICommand SaveAccountEditChangesCommand { get; }
    }
}

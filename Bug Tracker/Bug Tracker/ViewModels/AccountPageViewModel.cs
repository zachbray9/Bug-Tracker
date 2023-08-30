using Bug_Tracker.State.Authenticators;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Bug_Tracker.ViewModels
{
    public class AccountPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        private readonly IUserService UserDataService;

        public User CurrentUser { get => Authenticator.CurrentUser; }

        public AccountPageViewModel(IAuthenticator authenticator, IUserService userDataService)
        {
            Authenticator = authenticator;
            UserDataService = userDataService;      

            firstNameTextboxText = CurrentUser.FirstName;
            lastNameTextboxText = CurrentUser.LastName;
            emailTextboxText = CurrentUser.Email;
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
                if (FirstNameTextboxText != CurrentUser.FirstName)
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
                if (LastNameTextboxText != CurrentUser.LastName)
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
                if (EmailTextboxText != CurrentUser.Email)
                    return true;

                return false;
            }
        }

        private async Task SaveChanges()
        {
            //check if any of the input boxes are null
            if(string.IsNullOrEmpty(FirstNameTextboxText) || string.IsNullOrEmpty(LastNameTextboxText) || string.IsNullOrEmpty(EmailTextboxText))
            { 
                //handle error
                return; 
            }

            //check if first name contains any specials characters
            if (!String.IsNullOrEmpty(FirstNameTextboxText))
            {
                foreach (char c in FirstNameTextboxText)
                {
                    if (!char.IsLetter(c))
                    {
                        firstNameTextboxText = CurrentUser.FirstName;
                        return;
                    }
                }
            }

            //check if last name contains any special characters
            if (!String.IsNullOrEmpty(LastNameTextboxText))
            {
                foreach (char c in LastNameTextboxText)
                {
                    if (!char.IsLetter(c))
                    {
                        lastNameTextboxText = CurrentUser.LastName;
                        return;
                    }
                }
            }

            //check if email is in a valid format
            if(!IsValidEmail(EmailTextboxText))
            {

                emailTextboxText = CurrentUser.Email;
                return;
            }

            //if all cases pass then save changes
            CurrentUser.FirstName = FirstNameTextboxText;
            CurrentUser.LastName = LastNameTextboxText;
            CurrentUser.Email = EmailTextboxText;

            try
            {
                await UserDataService.Update(CurrentUser.Id, CurrentUser);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
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

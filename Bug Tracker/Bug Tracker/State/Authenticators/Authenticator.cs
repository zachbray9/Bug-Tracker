﻿using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bug_Tracker.State.Authenticators
{
    public class Authenticator : IAuthenticator, INotifyPropertyChanged
    {
        private readonly IAuthenticationService AuthenticationService;

        public event PropertyChangedEventHandler PropertyChanged;

        private User currentUser;
        public User CurrentUser
        {
            get
            {
                return currentUser;
            }

            private set
            {
                currentUser = value;
                OnPropertyChanged(nameof(currentUser));
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }
        public bool IsLoggedIn => CurrentUser != null;


        public Authenticator(IAuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService;
        }

        public async Task<bool> Login(string email, string password)
        {
            bool success = true;

            try
            {
                CurrentUser = await AuthenticationService.Login(email, password);
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public async Task<RegistrationResult> CreateAccount(string email, string firstName, string lastName, string password, string confirmPassword)
        {
            return await AuthenticationService.CreateAccount(email, firstName, lastName, password, confirmPassword);
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

﻿using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.State;
using Bug_Tracker.Utility_Classes;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Enumerables.Enum_Converters;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class AddUserToProjectPopupViewModel : ViewModelBase
    {
        private readonly IUserApiService UserApiService;
        private readonly IProjectUserApiService ProjectUserApiService;
        private readonly IProjectContainer ProjectContainer;
        private readonly ProjectRoleOptionsRetriever ProjectRoleOptionsRetriever;

        public Dictionary<ProjectRole, string> ProjectRoleOptionsDictionary { get => ProjectRoleOptionsRetriever.ProjectRoleOptionsDictionary; }

        public AddUserToProjectPopupViewModel(IUserApiService userApiService, IProjectUserApiService projectUserApiService, IProjectContainer projectContainer, ProjectRoleOptionsRetriever projectRoleOptionsRetriever)
        {
            UserApiService = userApiService;
            ProjectUserApiService = projectUserApiService;
            ProjectContainer = projectContainer;
            ProjectRoleOptionsRetriever = projectRoleOptionsRetriever;

            SelectedProjectRoleAsString = ProjectRoleOptionsRetriever.ConvertProjectRoleEnumToString(ProjectRole.Developer);
            UserInputIsEnabled = true;

            AddUserToProjectCommand = new AddUserToProjectCommand(UserApiService, ProjectUserApiService, ProjectContainer, this);
            CloseAddUserPopupCommand = new CloseAddUserPopupCommand(this);
        }

        private bool isPopupOpen;
        public bool IsPopupOpen
        {
            get { return isPopupOpen; }
            set
            {
                isPopupOpen = value;
                OnPropertyChanged(nameof(IsPopupOpen));
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

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                searchQuery = value;
                IsSearchResultsPopupOpen = IsSearchQueryPopulated;
                UpdateSearchResults();
                OnPropertyChanged(nameof(IsSearchQueryPopulated));
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        public bool IsSearchQueryPopulated
        {
            get
            {
                if (String.IsNullOrEmpty(SearchQuery))
                {
                    return false;
                }

                return true;
            }
        }

        private bool isSearchResultsPopupOpen;
        public bool IsSearchResultsPopupOpen
        {
            get { return isSearchResultsPopupOpen; }
            set
            {
                isSearchResultsPopupOpen = value;
                OnPropertyChanged(nameof(IsSearchResultsPopupOpen));
            }
        }

        public ObservableCollection<UserSearchResult> SearchResults { get; set; } = new ObservableCollection<UserSearchResult>();

        private UserSearchResult selectedUser;
        public UserSearchResult SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if(selectedUser != value)
                {
                    selectedUser = value;
                    if(value != null)
                    {
                        SearchQuery = value.Text;
                    }
                    IsSearchResultsPopupOpen = false;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public ProjectRole SelectedProjectRoleAsEnum { get => ProjectRoleOptionsRetriever.ConvertProjectRoleStringToEnum(SelectedProjectRoleAsString); }
        private string selectedProjectRoleAsString;
        public string SelectedProjectRoleAsString
        {
            get { return selectedProjectRoleAsString; }
            set
            {
                selectedProjectRoleAsString = value;
                OnPropertyChanged(nameof(SelectedProjectRoleAsString));
            }
        }

        private async void UpdateSearchResults()
        {
            SearchResults.Clear();

            if (String.IsNullOrEmpty(SearchQuery))
            {
                return;
            }

            IEnumerable<UserDTO> AllUsers = await UserApiService.GetAll();
            foreach(var user in AllUsers)
            {
                if(user.Email.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    SearchResults.Add(new UserSearchResult { Text = user.Email, IsEmail = true }); ;
                }
            }
        }

        public ICommand AddUserToProjectCommand { get; }
        public ICommand CloseAddUserPopupCommand { get; }
    }
}

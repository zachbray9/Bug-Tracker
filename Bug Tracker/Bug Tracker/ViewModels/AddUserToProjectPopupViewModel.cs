using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.Utility_Classes;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class AddUserToProjectPopupViewModel : ViewModelBase
    {
        private readonly IUserService UserDataService;

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


        public AddUserToProjectPopupViewModel(IUserService userDataService)
        {
            UserDataService = userDataService;

            CloseAddUserPopupCommand = new CloseAddUserPopupCommand(this);
        }

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                searchQuery = value;
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
                    return false;

                return true;
            }
        }

        public ObservableCollection<UserSearchResult> SearchResults { get; set; } = new ObservableCollection<UserSearchResult>();

        private UserSearchResult selectedUser;
        public UserSearchResult SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                searchQuery = value?.Text;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        private async void UpdateSearchResults()
        {
            SearchResults.Clear();

            if (String.IsNullOrEmpty(SearchQuery))
            {
                return;
            }

            IEnumerable<User> AllUsers = await UserDataService.GetAll();
            foreach(var user in AllUsers)
            {

                if(user.FullName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    SearchResults.Add(new UserSearchResult { Text = user.FullName});
                }

                if(user.Email.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    SearchResults.Add(new UserSearchResult { Text = user.Email, IsEmail = true }); ;
                }


            }
        }

        public ICommand CloseAddUserPopupCommand { get; }
    }
}

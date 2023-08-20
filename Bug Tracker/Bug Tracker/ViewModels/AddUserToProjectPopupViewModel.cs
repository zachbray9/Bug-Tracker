using Bug_Tracker.Commands.ProjectsPage_Commands;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
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
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        public ObservableCollection<User> SearchResults { get; set; } = new ObservableCollection<User>();

        private async void UpdateSearchResults()
        {
            SearchResults.Clear();

            IEnumerable<User> AllUsers = await UserDataService.GetAll();
            foreach(var user in AllUsers)
            {
                if(user.FullName.Contains(searchQuery) || user.Email.Contains(searchQuery))
                {
                    SearchResults.Add(user);
                }


            }

        }

        public ICommand CloseAddUserPopupCommand { get; }
    }
}

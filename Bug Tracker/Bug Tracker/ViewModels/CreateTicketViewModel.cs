using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels
{
    public class CreateTicketViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        private readonly INavigator Navigator;

        public ObservableCollection<string> StatusOptions { get; set; }

        public User CurrentUser { get => Authenticator.CurrentUser; }
        public CreateTicketViewModel(IAuthenticator authenticator, INavigator navigator)
        {
            Authenticator = authenticator;
            Navigator = navigator;

            List<string> statusOptions = Enum.GetNames(typeof(Status)).Cast<string>().ToList();
            StatusOptions = new ObservableCollection<string>(statusOptions);
        }
    }
}

using Bug_Tracker.Commands.AccountCommands;
using Bug_Tracker.Commands.Navigation_Commands;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; set; }
        public IViewModelAbstractFactory ViewModelAbstractFactory { get; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator, IViewModelAbstractFactory viewModelAbstractFactory)
        {
            Navigator = navigator;
            Authenticator = authenticator;
            ViewModelAbstractFactory = viewModelAbstractFactory;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(Navigator, ViewModelAbstractFactory);
            OpenAccountPopupCommand = new OpenAccountPopupCommand(this);

            UpdateCurrentViewModelCommand.Execute(ViewType.LoginPage);
        }

        private bool isAccountPopupOpen;
        public bool IsAccountPopupOpen
        {
            get { return isAccountPopupOpen; }
            set
            {
                isAccountPopupOpen = value;
                OnPropertyChanged(nameof(IsAccountPopupOpen));
            }
        }

        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand OpenAccountPopupCommand { get; }
    }
}

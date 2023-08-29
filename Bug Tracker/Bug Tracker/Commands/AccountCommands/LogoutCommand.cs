using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.AccountCommands
{
    public class LogoutCommand : CommandBase
    {
        private readonly MainViewModel ViewModel;
        private readonly IProjectContainer ProjectContainer;

        private INavigator Navigator { get => ViewModel.Navigator; }
        private IAuthenticator Authenticator { get => ViewModel.Authenticator; }
   
        

        public LogoutCommand(MainViewModel viewModel, IProjectContainer projectContainer)
        {
            ViewModel = viewModel;
            ProjectContainer = projectContainer;
        }

        public override void Execute(object parameter)
        {
            ViewModel.IsAccountPopupOpen = false;
            ProjectContainer.CurrentTicket = null;
            ProjectContainer.CurrentProject = null;
            Authenticator.Logout();
            Navigator.Navigate(ViewType.LoginPage);
        }
    }
}

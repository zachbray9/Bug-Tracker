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
        private readonly ITicketContainer TicketContainer;

        private INavigator Navigator { get => ViewModel.Navigator; }
        private IAuthenticator Authenticator { get => ViewModel.Authenticator; }
   
        

        public LogoutCommand(MainViewModel viewModel, IProjectContainer projectContainer, ITicketContainer ticketContainer)
        {
            ViewModel = viewModel;
            ProjectContainer = projectContainer;
            TicketContainer = ticketContainer;
        }

        public override void Execute(object parameter)
        {
            ViewModel.IsAccountPopupOpen = false;
            ProjectContainer.CurrentProject = null;
            TicketContainer.CurrentTicket = null;
            TicketContainer.Assignee = null;
            TicketContainer.Author = null;
            Authenticator.Logout();
            Navigator.Navigate(ViewType.LoginPage);
        }
    }
}

using Bug_Tracker.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bug_Tracker.Commands.Navigation_Commands
{
    public class NavigateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigator Navigator;

        public NavigateCommand(INavigator navigator)
        {
            Navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //need to add as you create more views
            switch(parameter)
            {
                case "LoginPage":
                    Navigator.Navigate(ViewType.LoginPage);
                    break;
                case "CreateAccountPage":
                    Navigator.Navigate(ViewType.CreateAccountPage);
                    break;
                default:
                    MessageBox.Show("The ViewType you put in the command parameters does not exist", "Invalid ViewType", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            
           
        }
    }
}

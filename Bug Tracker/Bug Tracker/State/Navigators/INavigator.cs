using Bug_Tracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bug_Tracker.State.Navigators
{
    public enum ViewType
    {
        LoginPage,
        CreateAccountPage,
        HomePage,
        AccountPage,
        ProjectsPage,
        TicketsPage
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        void Navigate(ViewType viewType);
        ICommand NavigateCommand { get; }
    }
}

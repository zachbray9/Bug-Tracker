using Bug_Tracker.Stores;
using Bug_Tracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Bug_Tracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore NavigationStore = new NavigationStore();
            NavigationStore.CurrentViewModel = new LoginPageViewModel();

            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(NavigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

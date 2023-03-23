using Bug_Tracker.ViewModels;
using Bug_Tracker.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.State.Navigators
{
    public class Renavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
    {
        private readonly INavigator Navigator;
        private readonly IViewModelFactory<TViewModel> ViewModelFactory;

        public Renavigator(INavigator navigator, IViewModelFactory<TViewModel> viewModelFactory)
        {
            Navigator = navigator;
            ViewModelFactory = viewModelFactory;
        }

        public void Renavigate()
        {

            Navigator.CurrentViewModel = ViewModelFactory.CreateViewModel();
        }
    }
}

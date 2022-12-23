using Bug_Tracker.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore NavigationStore;
        public ViewModelBase CurrentViewModel => NavigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            NavigationStore = navigationStore;
            NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

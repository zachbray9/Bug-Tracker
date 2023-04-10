using Bug_Tracker.Commands.Navigation_Commands;
using Bug_Tracker.ViewModels;
using Bug_Tracker.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bug_Tracker.State.Navigators
{
    public class Navigator : INavigator, INotifyPropertyChanged
    {
        private readonly IViewModelAbstractFactory ViewModelAbstractFactory;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public Navigator(IViewModelAbstractFactory viewModelAbstractFactory)
        {
            ViewModelAbstractFactory= viewModelAbstractFactory;

            NavigateCommand = new NavigateCommand(this);
        }

        public void Navigate(ViewType viewType)
        {
            CurrentViewModel = ViewModelAbstractFactory.CreateViewModel(viewType);
        }

        public ICommand NavigateCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

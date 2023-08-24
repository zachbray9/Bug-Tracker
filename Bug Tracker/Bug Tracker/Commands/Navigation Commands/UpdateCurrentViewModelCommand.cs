using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using Bug_Tracker.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bug_Tracker.Commands.Navigation_Commands
{
    public class UpdateCurrentViewModelCommand : CommandBase
    {
        private readonly INavigator Navigator;
        private readonly IViewModelAbstractFactory ViewModelAbstractFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelAbstractFactory viewModelAbstractFactory)
        {
            Navigator = navigator;
            ViewModelAbstractFactory = viewModelAbstractFactory;
        }

        public override void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                Navigator.CurrentViewModel = ViewModelAbstractFactory.CreateViewModel(viewType);

            }
        }
    }
}

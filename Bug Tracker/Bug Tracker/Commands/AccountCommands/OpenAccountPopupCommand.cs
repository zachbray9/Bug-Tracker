using Bug_Tracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.AccountCommands
{
    public class OpenAccountPopupCommand : CommandBase
    {
        private readonly MainViewModel ViewModel;

        public OpenAccountPopupCommand(MainViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            ViewModel.IsAccountPopupOpen = true;
        }
    }
}

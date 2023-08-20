using Bug_Tracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class CloseAddUserPopupCommand : CommandBase
    {
        private readonly AddUserToProjectPopupViewModel AddUserViewModel;

        public CloseAddUserPopupCommand(AddUserToProjectPopupViewModel addUserViewModel)
        {
               AddUserViewModel = addUserViewModel;
        }

        public override void Execute(object parameter)
        {
            AddUserViewModel.IsPopupOpen = false;
        }
    }
}

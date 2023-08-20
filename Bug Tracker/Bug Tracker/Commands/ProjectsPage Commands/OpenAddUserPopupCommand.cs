using Bug_Tracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class OpenAddUserPopupCommand : CommandBase
    {
        private readonly AddUserToProjectPopupViewModel AddUserViewModel;
        public OpenAddUserPopupCommand(AddUserToProjectPopupViewModel addUserViewModel)
        {
              AddUserViewModel = addUserViewModel;
        }

        public override void Execute(object parameter)
        {
            AddUserViewModel.IsPopupOpen = true;
        }
    }
}

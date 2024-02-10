using Bug_Tracker.ViewModels;

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

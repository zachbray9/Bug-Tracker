using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models.DTOs;

namespace Bug_Tracker.Commands.AccountCommands
{
    public class CancelAccountEditCommand : CommandBase
    {
        private readonly AccountPageViewModel ViewModel;
        private UserDTO CurrentUser { get => ViewModel.CurrentUser; }

        public CancelAccountEditCommand(AccountPageViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            ViewModel.FirstNameTextboxText = CurrentUser.FirstName;
            ViewModel.LastNameTextboxText = CurrentUser.LastName;
            ViewModel.EmailTextboxText = CurrentUser.Email;
        }
    }
}

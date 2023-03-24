using Bug_Tracker.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels.Factories
{
    public class ViewModelAbstractFactory : IViewModelAbstractFactory
    {
        private readonly IViewModelFactory<LoginPageViewModel> LoginPageViewModelFactory;
        private readonly IViewModelFactory<CreateAccountPageViewModel> CreateAccountPageViewModelFactory;

        public ViewModelAbstractFactory(IViewModelFactory<LoginPageViewModel> loginPageViewModelFactory, IViewModelFactory<CreateAccountPageViewModel> createAccountPageViewModelFactory)
        {
            LoginPageViewModelFactory= loginPageViewModelFactory;
            CreateAccountPageViewModelFactory = createAccountPageViewModelFactory;
        }
        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            //need to keep adding as you create more views
            switch (viewType)
            {
                case ViewType.LoginPage:
                    return LoginPageViewModelFactory.CreateViewModel();
                case ViewType.CreateAccountPage:
                    return CreateAccountPageViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The view type does not have a ViewModel.", "viewType"); hello
            }
        }
    }
}

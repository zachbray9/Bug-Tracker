using Bug_Tracker.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bug_Tracker.ViewModels.ViewModelBase;

namespace Bug_Tracker.ViewModels.Factories
{
    public class ViewModelAbstractFactory : IViewModelAbstractFactory
    {
        //these are delegates (on the viewmodelbase class) that are basically just functions
        private readonly CreateViewModel<LoginPageViewModel> CreateLoginPageViewModel;
        private readonly CreateViewModel<CreateAccountPageViewModel> CreateCreateAccountPageViewModel;
        private readonly CreateViewModel<HomePageViewModel> CreateHomePageViewModel;

        public ViewModelAbstractFactory(CreateViewModel<LoginPageViewModel> createLoginPageViewModel, CreateViewModel<CreateAccountPageViewModel> createCreateAccountPageViewModel, CreateViewModel<HomePageViewModel> createHomePageViewModel)
        {
            CreateLoginPageViewModel = createLoginPageViewModel;
            CreateCreateAccountPageViewModel = createCreateAccountPageViewModel;
            CreateHomePageViewModel = createHomePageViewModel;
        }
        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            //need to keep adding as you create more views
            switch (viewType)
            {
                case ViewType.LoginPage:
                    return CreateLoginPageViewModel();
                case ViewType.CreateAccountPage:
                    return CreateCreateAccountPageViewModel();
                case ViewType.HomePage:
                    return CreateHomePageViewModel();
                default:
                    throw new ArgumentException("The view type does not have a ViewModel.", "viewType");
            }
        }
    }
}

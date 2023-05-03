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
        private readonly CreateViewModel<AccountPageViewModel> CreateAccountPageViewModel;
        private readonly CreateViewModel<ProjectsPageViewModel> CreateProjectsPageViewModel;
        private readonly CreateViewModel<TicketsPageViewModel> CreateTicketsPageViewModel;
        private readonly CreateViewModel<CreateNewProjectPageViewModel> CreateCreateNewProjectPageViewModel;
        private readonly CreateViewModel<ProjectDetailsPageViewModel> CreateProjectDetailsPageViewModel;

        public ViewModelAbstractFactory(CreateViewModel<LoginPageViewModel> createLoginPageViewModel, CreateViewModel<CreateAccountPageViewModel> createCreateAccountPageViewModel, CreateViewModel<HomePageViewModel> createHomePageViewModel, CreateViewModel<AccountPageViewModel> createAccountPageViewModel, CreateViewModel<ProjectsPageViewModel> createProjectsPageViewModel, CreateViewModel<TicketsPageViewModel> createTicketsPageViewModel, CreateViewModel<CreateNewProjectPageViewModel> createCreateNewProjectPageViewModel, CreateViewModel<ProjectDetailsPageViewModel> createProjectDetailsPageViewModel)
        {
            CreateLoginPageViewModel = createLoginPageViewModel;
            CreateCreateAccountPageViewModel = createCreateAccountPageViewModel;
            CreateHomePageViewModel = createHomePageViewModel;
            CreateAccountPageViewModel = createAccountPageViewModel;
            CreateProjectsPageViewModel = createProjectsPageViewModel;
            CreateTicketsPageViewModel = createTicketsPageViewModel;
            CreateCreateNewProjectPageViewModel = createCreateNewProjectPageViewModel;
            CreateProjectDetailsPageViewModel = createProjectDetailsPageViewModel;
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
                case ViewType.AccountPage:
                    return CreateAccountPageViewModel();
                case ViewType.ProjectsPage:
                    return CreateProjectsPageViewModel();
                case ViewType.TicketsPage:
                    return CreateTicketsPageViewModel();
                case ViewType.CreateNewProjectPage:
                    return CreateCreateNewProjectPageViewModel();
                case ViewType.ProjectDetailsPage:
                    return CreateProjectDetailsPageViewModel();
                default:
                    throw new ArgumentException("The view type does not have a ViewModel.", "viewType");
            }
        }
    }
}

using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using Bug_Tracker.ViewModels.Factories;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using BugTracker.EntityFramework;
using BugTracker.EntityFramework.Services;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static Bug_Tracker.ViewModels.ViewModelBase;

namespace Bug_Tracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();


            services.AddSingleton<BugTrackerDbContextFactory>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDataService<User>, UserDataService>();

            services.AddSingleton<IDataService<Project>, GenericDataService<Project>>();
            services.AddSingleton<IDataService<ProjectUser>, GenericDataService<ProjectUser>>();

            services.AddSingleton<IUserService, UserDataService>();
            services.AddSingleton<IProjectService, ProjectDataService>();
            services.AddSingleton<IProjectUserService, ProjectUserDataService>();
            services.AddSingleton<ITicketService, TicketDataService>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddSingleton<IViewModelAbstractFactory, ViewModelAbstractFactory>();


            //Registering all of the viewmodels for dependency injection//
            services.AddSingleton<CreateViewModel<LoginPageViewModel>>(services =>
            {
                return () => new LoginPageViewModel(services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>());
            }
                );

            services.AddSingleton<CreateViewModel<CreateAccountPageViewModel>>(services =>
            {
                return () => new CreateAccountPageViewModel(services.GetRequiredService<INavigator>(), services.GetRequiredService<IAuthenticator>());
            }
            );

            services.AddSingleton<CreateViewModel<HomePageViewModel>>(services =>
            {
                return () => new HomePageViewModel(services.GetRequiredService<IAuthenticator>());
            }
            );

            services.AddSingleton<CreateViewModel<AccountPageViewModel>>(services =>
            {
                return () => new AccountPageViewModel();
            }
            );

            services.AddSingleton<CreateViewModel<ProjectsPageViewModel>>(services =>
            {
                return () => new ProjectsPageViewModel(services.GetRequiredService<IProjectService>(), services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>(), services.GetRequiredService<IProjectContainer>());
            }
            );

            services.AddSingleton<CreateViewModel<TicketsPageViewModel>>(services =>
            {
                return () => new TicketsPageViewModel(services.GetRequiredService<IAuthenticator>());
            }
            );

            services.AddSingleton<CreateViewModel<CreateNewProjectPageViewModel>>(services =>
            {
                return () => new CreateNewProjectPageViewModel(services.GetRequiredService<BugTrackerDbContextFactory>(), services.GetRequiredService<IDataService<Project>>(), services.GetRequiredService<IDataService<ProjectUser>>(), services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>());
            }
            );

            services.AddSingleton<CreateViewModel<ProjectDetailsPageViewModel>>(services =>
            {
                return () => new ProjectDetailsPageViewModel(services.GetRequiredService<IUserService>(), services.GetRequiredService<IProjectUserService>(), services.GetRequiredService<ITicketService>(), services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>(), services.GetRequiredService<IProjectContainer>());
            }
            );



            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IProjectContainer, ProjectContainer>();
            services.AddSingleton<MainViewModel>();

            services.AddScoped(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}

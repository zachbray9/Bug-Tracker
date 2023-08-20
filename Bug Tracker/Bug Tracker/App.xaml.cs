using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using Bug_Tracker.ViewModels.Factories;
using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using BugTracker.EntityFramework;
using BugTracker.EntityFramework.Services;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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

            //Registering the DbContext and its options
            services.AddDbContext<BugTrackerDbContext>(options =>
                {
                    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BugTrackerDB;Trusted_Connection=true");
                    options.EnableSensitiveDataLogging(true);
                    options.UseLazyLoadingProxies();
                }, ServiceLifetime.Scoped);

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IUserService, UserDataService>();

            services.AddSingleton<IDataService<Project>, GenericDataService<Project>>();
            services.AddSingleton<IDataService<ProjectUser>, GenericDataService<ProjectUser>>();
            services.AddSingleton<IDataService<Ticket>, GenericDataService<Ticket>>();
            services.AddSingleton<IDataService<Comment>, GenericDataService<Comment>>();
            
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<DispatcherTimer>();
            services.AddSingleton<StatusOptionsRetriever>();
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
                return () => new ProjectsPageViewModel(services.GetRequiredService<IDataService<Project>>(), services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>(), services.GetRequiredService<IProjectContainer>());
            }
            );

            services.AddSingleton<CreateViewModel<TicketsPageViewModel>>(services =>
            {
                return () => new TicketsPageViewModel(services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>(), services.GetRequiredService<IProjectContainer>(), services.GetRequiredService<StatusOptionsRetriever>());
            }
            );

            services.AddSingleton<CreateViewModel<CreateNewProjectPageViewModel>>(services =>
            {
                return () => new CreateNewProjectPageViewModel(services.GetRequiredService<BugTrackerDbContext>(), services.GetRequiredService<IDataService<Project>>(), services.GetRequiredService<IDataService<ProjectUser>>(), services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>());
            }
            );

            services.AddSingleton<CreateViewModel<AddUserToProjectPopupViewModel>>(services =>
            {
                return () => new AddUserToProjectPopupViewModel(services.GetRequiredService<IUserService>());
            }
            );

            services.AddSingleton<CreateViewModel<ProjectDetailsPageViewModel>>(services =>
            {
                return () => new ProjectDetailsPageViewModel(services.GetRequiredService<IUserService>(), services.GetRequiredService<IDataService<ProjectUser>>(), services.GetRequiredService<IDataService<Ticket>>(), services.GetRequiredService<IDataService<Comment>>(), services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>(), services.GetRequiredService<IProjectContainer>(), services.GetRequiredService<IViewModelAbstractFactory>());
            }
            );

            services.AddSingleton<CreateViewModel<CreateTicketViewModel>>(services =>
            {
                return () => new CreateTicketViewModel(services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>(), services.GetRequiredService<IProjectContainer>(), services.GetRequiredService<IDataService<Ticket>>(), services.GetRequiredService<StatusOptionsRetriever>());
            }
            );

            services.AddSingleton<CreateViewModel<TicketDetailsPageViewModel>>(services =>
            {
                return () => new TicketDetailsPageViewModel(services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<IProjectContainer>(), services.GetRequiredService<IDataService<Ticket>>(), services.GetRequiredService<IDataService<Comment>>(), services.GetRequiredService<DispatcherTimer>(), services.GetRequiredService<StatusOptionsRetriever>());
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

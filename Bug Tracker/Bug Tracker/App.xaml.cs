using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using Bug_Tracker.ViewModels.Factories;
using BugTracker.Domain.Enumerables.Enum_Converters;
using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using BugTracker.EntityFramework;
using BugTracker.EntityFramework.Services;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly IHost host;

        public App()
        {
            host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    string connectionString = context.Configuration.GetConnectionString("sqlite");

                    //Registering the DbContext and its options
                    services.AddDbContext<BugTrackerDbContext>(options =>
                    {
                        options.UseSqlite(connectionString);
                        options.EnableSensitiveDataLogging(true);
                        options.UseLazyLoadingProxies();
                    }, ServiceLifetime.Scoped);

                    services.AddSingleton<BugTrackerDbContextFactory>(new BugTrackerDbContextFactory(connectionString));

                    services.AddScoped<IAuthenticationService, AuthenticationService>();
                    services.AddScoped<IUserService, UserDataService>();

                    services.AddScoped<IDataService<Project>, GenericDataService<Project>>();
                    services.AddScoped<IDataService<ProjectUser>, GenericDataService<ProjectUser>>();
                    services.AddScoped<IDataService<Ticket>, GenericDataService<Ticket>>();
                    services.AddScoped<IDataService<Comment>, GenericDataService<Comment>>();

                    services.AddSingleton<IPasswordHasher, PasswordHasher>();
                    services.AddSingleton<DispatcherTimer>();
                    services.AddSingleton<StatusOptionsRetriever>();
                    services.AddSingleton<ProjectRoleOptionsRetriever>();
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
                        return () => new AccountPageViewModel(services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<IUserService>());
                    }
                    );

                    services.AddSingleton<CreateViewModel<ProjectsPageViewModel>>(services =>
                    {
                        return () => new ProjectsPageViewModel(services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>(), services.GetRequiredService<IProjectContainer>());
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
                        return () => new AddUserToProjectPopupViewModel(services.GetRequiredService<IUserService>(), services.GetRequiredService<IDataService<ProjectUser>>(), services.GetRequiredService<IProjectContainer>(), services.GetRequiredService<ProjectRoleOptionsRetriever>());
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
                        return () => new TicketDetailsPageViewModel(services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>(), services.GetRequiredService<IProjectContainer>(), services.GetRequiredService<IDataService<Ticket>>(), services.GetRequiredService<IDataService<Comment>>(), services.GetRequiredService<StatusOptionsRetriever>());
                    }
                    );




                    services.AddScoped<INavigator, Navigator>();
                    services.AddScoped<IAuthenticator, Authenticator>();
                    services.AddScoped<IProjectContainer, ProjectContainer>();
                    services.AddScoped<MainViewModel>();

                    services.AddScoped(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            host.Start();

            BugTrackerDbContext context = host.Services.GetRequiredService<BugTrackerDbContext>();
            context.Database.Migrate();

            Window window = host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();

            base.OnExit(e);
        }

        
    }
}

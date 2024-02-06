using Bug_Tracker.Services.Api;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using Bug_Tracker.ViewModels.Factories;
using BugTracker.Domain.Enumerables.Enum_Converters;
using BugTracker.Domain.Enumerables.EnumConverters;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.Api;
using BugTracker.Domain.Services.AuthenticationServices;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
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
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddJsonFile("appsettings.json");

                })
                .ConfigureServices((context, services) =>
                {
                    //Registering all of my services
                    services.AddHttpClient();

                    services.AddScoped<INavigator, Navigator>();
                    services.AddScoped<IAuthenticator, Authenticator>();
                    services.AddScoped<IProjectContainer, ProjectContainer>();
                    services.AddScoped<IAuthenticationService, AuthenticationService>();
                    services.AddScoped<IApiService<UserDTO>, UserApiService>();
                    services.AddScoped<IApiService<ProjectUserDTO>, ProjectUserApiService>();
                    services.AddScoped<IApiService<ProjectDTO>, ProjectApiService>();
                    services.AddScoped<IApiService<TicketDTO>, TicketApiService>();
                    services.AddSingleton<IPasswordHasher, PasswordHasher>();
                    services.AddSingleton<StatusOptionsRetriever>();
                    services.AddSingleton<ProjectRoleOptionsRetriever>();


                    //Registering all of the viewmodels
                    services.AddSingleton<IViewModelAbstractFactory, ViewModelAbstractFactory>();

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
                        return () => new TicketsPageViewModel(services.GetRequiredService<IAuthenticator>(), services.GetRequiredService<INavigator>(), services.GetRequiredService<IProjectContainer>());
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

                    services.AddScoped<MainViewModel>();

                    //Registering the mainwindow view
                    services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
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

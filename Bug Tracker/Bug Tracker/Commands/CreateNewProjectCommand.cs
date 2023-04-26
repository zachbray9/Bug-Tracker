using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bug_Tracker.Commands
{
    public class CreateNewProjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly BugTrackerDbContextFactory DbContextFactory;
        private readonly IDataService<Project> ProjectDataService;
        private readonly IDataService<ProjectUser> ProjectUserDataService;
        private readonly INavigator Navigator;
        private readonly CreateNewProjectPageViewModel CreateNewProjectPageViewModel;

        public CreateNewProjectCommand(BugTrackerDbContextFactory dbContextFactory, IDataService<Project> projectDataService, IDataService<ProjectUser> projectUserDataService, INavigator navigator, CreateNewProjectPageViewModel createNewProjectPageViewModel)
        {
            DbContextFactory = dbContextFactory;
            ProjectDataService = projectDataService;
            ProjectUserDataService = projectUserDataService;
            Navigator = navigator;
            CreateNewProjectPageViewModel= createNewProjectPageViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if(CreateNewProjectPageViewModel.ProjectName== null)
            {
                MessageBox.Show("You need to add a project name.", "Missing Project Name", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(CreateNewProjectPageViewModel.ProjectDescription== null)
            {
                MessageBox.Show("You need to add a project description.", "Missing Project Description", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Project project = await ProjectDataService.Create(new Project { Name = CreateNewProjectPageViewModel.ProjectName, Description = CreateNewProjectPageViewModel.ProjectDescription, DateStarted = DateTime.Now });
            ProjectUser projectUser = await ProjectUserDataService.Create(new ProjectUser { ProjectId = project.Id, UserId = CreateNewProjectPageViewModel.Authenticator.CurrentUser.Id});

            using(var db = DbContextFactory.CreateDbContext())
            {
                if (CreateNewProjectPageViewModel.Authenticator.CurrentUser.ProjectUsers == null)
                {
                    CreateNewProjectPageViewModel.Authenticator.CurrentUser.ProjectUsers = new List<ProjectUser>();
                }

                CreateNewProjectPageViewModel.Authenticator.CurrentUser.ProjectUsers.Add(projectUser);
                await db.SaveChangesAsync();
            }

            Navigator.Navigate(ViewType.ProjectsPage);
        }
    }
}

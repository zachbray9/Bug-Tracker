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
        private readonly BugTrackerDbContext DbContext;
        private readonly IDataService<Project> ProjectDataService;
        private readonly IDataService<ProjectUser> ProjectUserDataService;
        private readonly INavigator Navigator;
        private readonly CreateNewProjectPageViewModel CreateNewProjectPageViewModel;
        private User CurrentUser => CreateNewProjectPageViewModel.Authenticator.CurrentUser;

        public CreateNewProjectCommand(BugTrackerDbContext dbContext, IDataService<Project> projectDataService, IDataService<ProjectUser> projectUserDataService, INavigator navigator, CreateNewProjectPageViewModel createNewProjectPageViewModel)
        {
            DbContext = dbContext;
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
            ProjectUser projectUser = await ProjectUserDataService.Create(new ProjectUser { ProjectId = project.Id, UserId = CurrentUser.Id});

            

            if (CurrentUser.ProjectUsers == null)
            {
                CurrentUser.ProjectUsers = new List<ProjectUser>();
            }

                CurrentUser.ProjectUsers.Add(projectUser);
                DbContext.SaveChanges();
            

            Navigator.Navigate(ViewType.ProjectsPage);
        }
    }
}

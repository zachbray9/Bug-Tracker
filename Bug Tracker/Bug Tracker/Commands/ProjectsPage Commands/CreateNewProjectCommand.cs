using Bug_Tracker.State;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Windows;

namespace Bug_Tracker.Commands
{
    public class CreateNewProjectCommand : CommandBase
    {
        private readonly IApiService<ProjectDTO> ProjectApiService;
        private readonly IApiService<ProjectUserDTO> ProjectUserApiService;
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;
        private readonly CreateNewProjectPageViewModel CreateNewProjectPageViewModel;
        private UserDTO CurrentUser => CreateNewProjectPageViewModel.Authenticator.CurrentUser;

        public CreateNewProjectCommand(IApiService<ProjectDTO> projectApiService, IApiService<ProjectUserDTO> projectUserApiService, INavigator navigator, IProjectContainer projectContainer, CreateNewProjectPageViewModel createNewProjectPageViewModel)
        {
            ProjectApiService = projectApiService;
            ProjectUserApiService = projectUserApiService;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            CreateNewProjectPageViewModel= createNewProjectPageViewModel;
        }

        public override async void Execute(object parameter)
        {
            if (string.IsNullOrEmpty(CreateNewProjectPageViewModel.ProjectName))
            {
                MessageBox.Show("You need to add a project name.", "Missing Project Name", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(CreateNewProjectPageViewModel.ProjectDescription))
            {
                MessageBox.Show("You need to add a project description.", "Missing Project Description", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProjectDTO project = await ProjectApiService.Create(new ProjectDTO { Name = CreateNewProjectPageViewModel.ProjectName, Description = CreateNewProjectPageViewModel.ProjectDescription, DateStarted = DateTime.Now });
            ProjectUserDTO projectUser = await ProjectUserApiService.Create(new ProjectUserDTO { ProjectId = project.Id, UserId = CurrentUser.Id , Role = 0});
            ProjectContainer.CurrentUserProjects.Add(project);

            Navigator.Navigate(ViewType.ProjectsPage);
        }
    }
}

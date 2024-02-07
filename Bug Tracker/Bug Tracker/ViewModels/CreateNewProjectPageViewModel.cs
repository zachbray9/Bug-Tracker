using Bug_Tracker.Commands;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class CreateNewProjectPageViewModel : ViewModelBase
    {
        private readonly IApiService<ProjectDTO> ProjectApiService; 
        private readonly IApiService<ProjectUserDTO> ProjectUserApiService;
        public IAuthenticator Authenticator { get; }
        public INavigator Navigator { get; }

        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        private string projectDescription;
        public string ProjectDescription
        {
            get { return projectDescription; }
            set { projectDescription = value; }
        }

        private DateTime dateCreated;
        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

        public CreateNewProjectPageViewModel(IApiService<ProjectDTO> projectApiService, IApiService<ProjectUserDTO> projectUserApiService, IAuthenticator authenticator, INavigator navigator)
        {
            ProjectApiService = projectApiService;
            ProjectUserApiService = projectUserApiService;
            Authenticator = authenticator;
            Navigator = navigator;

            CreateNewProjectCommand = new CreateNewProjectCommand(ProjectApiService, ProjectUserApiService, Navigator, this);
        }

        public ICommand CreateNewProjectCommand { get; }
    }
}

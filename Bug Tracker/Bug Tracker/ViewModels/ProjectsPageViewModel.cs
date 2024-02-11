﻿using Bug_Tracker.State.Authenticators;
using System;
using System.Collections.ObjectModel;
using Bug_Tracker.State.Navigators;
using System.Windows.Input;
using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.State;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;

namespace Bug_Tracker.ViewModels
{
    public class ProjectsPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; }
        private readonly IProjectContainer ProjectContainer;
        private readonly IProjectApiService ProjectApiService;

        private ObservableCollection<ProjectDTO> projects;
        public ObservableCollection<ProjectDTO> Projects
        {
            get { return projects; }
            set
            {
                projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        private ObservableCollection<ProjectDTO> projectSearchResults;
        public ObservableCollection<ProjectDTO> ProjectSearchResults
        {
            get { return projectSearchResults; }
            set
            {
                projectSearchResults = value;
                OnPropertyChanged(nameof(ProjectSearchResults));
            }
        }

        public ProjectsPageViewModel(IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer, IProjectApiService projectApiService)
        {
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            ProjectApiService = projectApiService;

            Projects = new ObservableCollection<ProjectDTO>();
            ViewProjectDetailsCommand = new ViewProjectDetailsCommand(Navigator, ProjectContainer, ProjectApiService);

            UpdateProjects();
            ProjectSearchResults = new ObservableCollection<ProjectDTO>(Projects);
            
        }

        private string projectSearchQuery = String.Empty;
        public string ProjectSearchQuery
        {
            get { return projectSearchQuery; }
            set
            {
                projectSearchQuery = value;
                OnPropertyChanged(nameof(ProjectSearchQuery));
                UpdateProjectSearchResults();
            }
        }

        private void UpdateProjects()
        {
            Projects.Clear();

            //foreach (ProjectUserDTO projectUser in Authenticator.CurrentUser.ProjectUsers)
            //{
            //    Projects.Add(projectUser.Project);
            //}

            Projects = new ObservableCollection<ProjectDTO>(ProjectContainer.CurrentUserProjects);
        }

        private void UpdateProjectSearchResults()
        {
            ProjectSearchResults.Clear();

            foreach(ProjectDTO project in Projects)
            {
                if(project.Name.Contains(ProjectSearchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    ProjectSearchResults.Add(project);
                }    
            }
        }

        public ICommand ViewProjectDetailsCommand { get; }

    }
}

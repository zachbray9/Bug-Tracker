using BugTracker.Domain.Models;
using Bug_Tracker.State.Authenticators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug_Tracker.State.Navigators;
using Microsoft.EntityFrameworkCore.Internal;
using BugTracker.EntityFramework;
using System.ComponentModel;
using BugTracker.Domain.Services;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Windows.Input;
using Bug_Tracker.Commands.ProjectsPage_Commands;
using Bug_Tracker.State;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.RightsManagement;

namespace Bug_Tracker.ViewModels
{
    public class ProjectsPageViewModel : ViewModelBase
    {
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; }
        private readonly IProjectContainer ProjectContainer;
        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set
            {
                projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        private ObservableCollection<Project> projectSearchResults;
        public ObservableCollection<Project> ProjectSearchResults
        {
            get { return projectSearchResults; }
            set
            {
                projectSearchResults = value;
                OnPropertyChanged(nameof(ProjectSearchResults));
            }
        }

        public ProjectsPageViewModel(IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer)
        {
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            Projects = new ObservableCollection<Project>();
            ViewProjectDetailsCommand = new ViewProjectDetailsCommand(Navigator, ProjectContainer);

            UpdateProjects();
            ProjectSearchResults = new ObservableCollection<Project>(Projects);
            
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

            foreach(ProjectUser projectUser in Authenticator.CurrentUser.ProjectUsers)
            {
                Projects.Add(projectUser.Project);
            }
        }

        private void UpdateProjectSearchResults()
        {
            ProjectSearchResults.Clear();

            foreach(Project project in Projects)
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

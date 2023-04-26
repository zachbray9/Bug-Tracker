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

namespace Bug_Tracker.ViewModels
{
    public class ProjectsPageViewModel : ViewModelBase
    {
        private readonly IDataService<Project> ProjectDataService;
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; }
        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects
        {
            get { return projects;}
            set 
            { 
                projects = value; 
                OnPropertyChanged(nameof(projects));
            }
        }

        public ProjectsPageViewModel(IDataService<Project> projectDataService, IAuthenticator authenticator, INavigator navigator)
        {
            ProjectDataService= projectDataService;
            Authenticator = authenticator;
            Navigator = navigator;
            Projects = new ObservableCollection<Project>();

            UpdateProjects();
        }

        private async void UpdateProjects()
        {
                    foreach(ProjectUser projectUser in Authenticator.CurrentUser.ProjectUsers)
                    {
                        Project project = await ProjectDataService.Get(projectUser.ProjectId);
                        projects.Add(project);
                    }        
        }

    }
}

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

namespace Bug_Tracker.ViewModels
{
    public class ProjectsPageViewModel : ViewModelBase
    {
        private readonly BugTrackerDbContextFactory DbContextFactory;
        private readonly IAuthenticator Authenticator;
        public INavigator Navigator { get; }
        public ObservableCollection<Project> Projects { get; set; }

        public ProjectsPageViewModel(BugTrackerDbContextFactory dbContextFactory, IAuthenticator authenticator, INavigator navigator)
        {
            DbContextFactory= dbContextFactory;
            Authenticator = authenticator;
            Navigator = navigator;
            Projects = new ObservableCollection<Project>();

            UpdateProjects();
        }

        private void UpdateProjects()
        {
        
                using(var db = DbContextFactory.CreateDbContext())
                {
                    //var projects = from project in db.Projects
                    //               from projectUser in db.ProjectUsers
                    //               where Authenticator.CurrentUser.ProjectUsers.Any(projectUser => projectUser.ProjectId == project.Id)
                    //               orderby project.DateStarted descending
                    //               select project;

                    //Projects = new ObservableCollection<Project>(projects);

                    foreach(ProjectUser projectUser in Authenticator.CurrentUser.ProjectUsers)
                    {
                        Projects.Add(projectUser.Project);
                    }
                }

            
        }
    }
}

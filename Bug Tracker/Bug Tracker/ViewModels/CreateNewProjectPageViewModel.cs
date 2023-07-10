using Bug_Tracker.Commands;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bug_Tracker.ViewModels
{
    public class CreateNewProjectPageViewModel : ViewModelBase
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly IDataService<Project> ProjectDataService; 
        private readonly IDataService<ProjectUser> ProjectUserDataService;
        public IAuthenticator Authenticator { get; }
        private readonly INavigator Navigator;

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

        public CreateNewProjectPageViewModel(BugTrackerDbContext dbContext, IDataService<Project> projectDataService, IDataService<ProjectUser> projectUserDataService, IAuthenticator authenticator, INavigator navigator)
        {
            DbContext = dbContext; 
            ProjectDataService = projectDataService;
            ProjectUserDataService = projectUserDataService;
            Authenticator = authenticator;
            Navigator = navigator;

            CreateNewProjectCommand = new CreateNewProjectCommand(DbContext, ProjectDataService, ProjectUserDataService, Navigator, this);
        }

        public ICommand CreateNewProjectCommand { get; }
    }
}

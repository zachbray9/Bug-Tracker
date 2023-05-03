using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Bug_Tracker.ViewModels
{
    public class ProjectDetailsPageViewModel : ViewModelBase
    {
        private readonly IUserService UserDataService;
        private readonly IAuthenticator Authenticator;
        private readonly INavigator Navigator;
        public IProjectContainer ProjectContainer { get; }

        public User CurrentUser { get => Authenticator.CurrentUser; }
        public Project CurrentProject { get => ProjectContainer.CurrentProject; }

        private ObservableCollection<ProjectUser> projectUsers;
        public ObservableCollection<ProjectUser> ProjectUsers
        {
            get
            {
                return projectUsers;
            }

            set
            {
                projectUsers = value;
                OnPropertyChanged(nameof(ProjectUsers));

            }
        }

        public ProjectDetailsPageViewModel(IUserService userDataService, IAuthenticator authenticator, INavigator navigator, IProjectContainer projectContainer)
        {
            UserDataService= userDataService;
            Authenticator = authenticator;
            Navigator = navigator;
            ProjectContainer = projectContainer;

            ProjectUsers = new ObservableCollection<ProjectUser>();
            UpdateProjectUsers();
        }

        private async void UpdateProjectUsers()
        {
            
                foreach(ProjectUser projectUser in CurrentProject.ProjectUsers)
                {
                    projectUser.User = await UserDataService.Get(projectUser.UserId);
                    ProjectUsers.Add(projectUser);
                }
            
                
        }
    }
}

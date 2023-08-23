using Bug_Tracker.State;
using Bug_Tracker.Utility_Classes;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class AddUserToProjectCommand : CommandBase
    {
        private readonly IUserService UserDataService;
        private readonly IDataService<ProjectUser> ProjectUserDataService;
        private readonly IProjectContainer ProjectContainer;
        private readonly AddUserToProjectPopupViewModel AddUserViewModel;

        private Project CurrentProject { get => ProjectContainer.CurrentProject; }
        private UserSearchResult SelectedUser { get => AddUserViewModel.SelectedUser; }
        private string SearchQuery { get => AddUserViewModel.SearchQuery; }
        private ProjectRole SelectedRole { get => AddUserViewModel.SelectedProjectRoleAsEnum; }

        public AddUserToProjectCommand(IUserService userDataService, IDataService<ProjectUser> projectUserDataService, IProjectContainer projectContainer, AddUserToProjectPopupViewModel addUserViewModel)
        {
            UserDataService = userDataService;
            ProjectUserDataService = projectUserDataService;
            ProjectContainer = projectContainer;
            AddUserViewModel = addUserViewModel;
        }

        public override async void Execute(object parameter)
        {
            User userToAdd = null;

            if(SearchQuery != null)
            {
                if(SearchQuery.Contains("@"))
                {
                    userToAdd = await UserDataService.GetByEmail(SearchQuery);
                }
                else
                {
                    userToAdd = await UserDataService.GetByFullName(SearchQuery);
                }
            }
            else
            {
                //handle if the search query is empty
                MessageBox.Show("The search query is empty. Please enter a valid Name or Email.", "Search Query Is Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if(userToAdd != null)
            {
                ProjectUser newProjectUser = new ProjectUser()
                {
                    UserId = userToAdd.Id,
                    ProjectId = CurrentProject.Id,
                    Role = SelectedRole,
                };

                //Check if the user that is being added already exists in the project
                if(CurrentProject.ProjectUsers.Any(pu => pu.UserId == newProjectUser.UserId))
                {
                    MessageBox.Show("The user you are trying to add is already in this project.", "User Already Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await ProjectUserDataService.Create(newProjectUser);
                AddUserViewModel.IsPopupOpen = false;
            }
            else
            {
                //handle if the search query was not found
                MessageBox.Show("The Name or Email you entered was not found. Please enter a valid Name or Email.", "User Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

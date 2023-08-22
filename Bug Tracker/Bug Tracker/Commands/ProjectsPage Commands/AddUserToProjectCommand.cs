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
            if(AddUserViewModel.SearchQuery.IsNullOrEmpty())
            {
                MessageBoxResult result = MessageBox.Show("Please enter a valid Users Name or Email.", "Invalid Search Query", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }


            User userToAdd = null;

            if(SelectedUser.IsEmail)
            {
                userToAdd = await UserDataService.GetByEmail(SelectedUser.Text);
            }
            else
            {
                userToAdd = await UserDataService.GetByFullName(SelectedUser.Text);
            }

            ProjectUser newProjectUser = new ProjectUser()
            {
                ProjectId = CurrentProject.Id,
                UserId= userToAdd.Id,
                Role = SelectedRole
            };

            await ProjectUserDataService.Create(newProjectUser);
            CurrentProject.ProjectUsers.Add(newProjectUser);
        }
    }
}

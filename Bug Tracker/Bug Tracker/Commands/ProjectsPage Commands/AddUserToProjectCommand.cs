using Bug_Tracker.State;
using Bug_Tracker.Utility_Classes;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using System.Linq;
using System.Windows;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class AddUserToProjectCommand : CommandBase
    {
        private readonly IUserApiService UserApiService;
        private readonly IApiService<ProjectUserDTO> ProjectUserApiService;
        private readonly IProjectContainer ProjectContainer;
        private readonly AddUserToProjectPopupViewModel AddUserViewModel;

        private ProjectDTO CurrentProject { get => ProjectContainer.CurrentProject; }
        private UserSearchResult SelectedUser { get => AddUserViewModel.SelectedUser; }
        private string SearchQuery { get => AddUserViewModel.SearchQuery; }
        private ProjectRole SelectedRole { get => AddUserViewModel.SelectedProjectRoleAsEnum; }

        public AddUserToProjectCommand(IUserApiService userApiService, IApiService<ProjectUserDTO> projectUserApiService, IProjectContainer projectContainer, AddUserToProjectPopupViewModel addUserViewModel)
        {
            UserApiService = userApiService;
            ProjectUserApiService = projectUserApiService;
            ProjectContainer = projectContainer;
            AddUserViewModel = addUserViewModel;
        }

        public override async void Execute(object parameter)
        {
            UserDTO userToAdd = null;

            if(SearchQuery != null)
            {
                if(SearchQuery.Contains("@"))
                {
                    userToAdd = await UserApiService.GetByEmail(SearchQuery);
                }
                else
                {
                    userToAdd = await UserApiService.GetByFullName(SearchQuery);
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
                ProjectUserDTO newProjectUser = new ProjectUserDTO()
                {
                    UserId = userToAdd.Id,
                    ProjectId = CurrentProject.Id,
                    Role = SelectedRole,
                };

                //Check if the user that is being added already exists in the project
                if(ProjectContainer.CurrentProjectUsers.Any(pu => pu.UserId == newProjectUser.UserId))
                {
                    MessageBox.Show("The user you are trying to add is already in this project.", "User Already Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await ProjectUserApiService.Create(newProjectUser);
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

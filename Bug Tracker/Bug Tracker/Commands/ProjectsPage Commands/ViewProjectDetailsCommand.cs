using Bug_Tracker.State;
using Bug_Tracker.State.Navigators;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class ViewProjectDetailsCommand : CommandBase
    {
        private readonly INavigator Navigator;
        private readonly IProjectContainer ProjectContainer;
        private readonly IProjectApiService ProjectApiService;
        public ViewProjectDetailsCommand(INavigator navigator, IProjectContainer projectContainer, IProjectApiService projectApiService)
        {
            Navigator = navigator;
            ProjectContainer = projectContainer;
            ProjectApiService = projectApiService;
        }
        public async override void Execute(object parameter)
        {
            ProjectDTO project = (ProjectDTO)parameter;
            if (project != null)
            {
                ProjectContainer.CurrentProject = project;
                ProjectContainer.CurrentTicketsOnProject = await ProjectApiService.GetAllTicketsOnProject(project.Id);
                Navigator.Navigate(ViewType.ProjectDetailsPage);
            }
        }
    }
}

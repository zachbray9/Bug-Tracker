using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class AddNewTicketToDbCommand : CommandBase
    {
        private readonly ITicketService TicketService;
        private readonly IUserService UserService;
        private readonly IProjectUserService ProjectUserService;
        private readonly ProjectDetailsPageViewModel ProjectDetailsPageViewModel;

        private User CurrentUser { get => ProjectDetailsPageViewModel.CurrentUser; }
        private Project CurrentProject { get => ProjectDetailsPageViewModel.CurrentProject; }

        public AddNewTicketToDbCommand(ITicketService ticketService, IUserService userService, IProjectUserService projectUserService, ProjectDetailsPageViewModel projectDetailsPageViewModel)
        {
            TicketService = ticketService;
            UserService = userService;
            ProjectUserService = projectUserService;
            ProjectDetailsPageViewModel = projectDetailsPageViewModel;
        }

        public async override void Execute(object parameter)
        {
            ProjectUser projectUser = await ProjectUserService.Get(CurrentUser.ProjectUsers.FirstOrDefault(pu => pu.ProjectId == CurrentProject.Id).Id);
            await TicketService.Create(new Ticket { Title = "", Description = "", Project = CurrentProject, ProjectId = CurrentProject.Id, Author = projectUser, AuthorId = projectUser.Id, Status = Status.ToDo, Priority = Priority.Low, DateSubmitted = DateTime.Now });
        }
    }
}

using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Navigators;
using Bug_Tracker.ViewModels;
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework;
using BugTracker.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Commands.ProjectsPage_Commands
{
    public class AddNewTicketToDbCommand : CommandBase
    {
        private readonly BugTrackerDbContextFactory DbContextFactory;
        private readonly User CurrentUser;
        public INavigator Navigator{ get; }
        private readonly IProjectContainer ProjectContainer;
        private readonly ITicketService TicketService;
        private readonly IProjectUserService ProjectUserService;
        private readonly CreateTicketViewModel CreateTicketViewModel;

        private Project CurrentProject { get => ProjectContainer.CurrentProject; }
        private Ticket CurrentTicket { get => ProjectContainer.CurrentTicket; }

        public AddNewTicketToDbCommand(BugTrackerDbContextFactory dbContextFactory, User currentUser, INavigator navigator, IProjectContainer projectContainer, ITicketService ticketService, IProjectUserService projectUserService, CreateTicketViewModel createTicketViewModel)
        {
            DbContextFactory = dbContextFactory;
            CurrentUser = currentUser;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            TicketService = ticketService;
            ProjectUserService = projectUserService;
            CreateTicketViewModel = createTicketViewModel;
        }

        public async override void Execute(object parameter)
        {
            ProjectUser projectUser = await ProjectUserService.Get(CurrentUser.ProjectUsers.FirstOrDefault(pu => pu.ProjectId == CurrentProject.Id).Id);

            using(var db = DbContextFactory.CreateDbContext())
            {
                int projectUserId = projectUser.Id;
                ProjectUser existingProjectUser = db.Set<ProjectUser>().Find(projectUserId);

                if (existingProjectUser != null) 
                {
                    var entry = db.Entry(existingProjectUser);
                    entry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                    ProjectContainer.CurrentTicket = await TicketService.Create(new Ticket { Title = CreateTicketViewModel.TicketTitle, Description = CreateTicketViewModel.TicketDescription, Project = CurrentProject, ProjectId = CurrentProject.Id, Author = existingProjectUser, AuthorId = existingProjectUser.Id, Status = CurrentTicket.Status, Priority = Priority.Low, DateSubmitted = DateTime.Now });
                    Navigator.Navigate(ViewType.ProjectDetailsPage);
                }
                else
                {
                    Console.WriteLine("Project User is null, so he's not being tracked.");
                }
            }

            //ProjectContainer.CurrentTicket = await TicketService.Create(new Ticket { Title = CreateTicketViewModel.TicketTitle, Description = CreateTicketViewModel.TicketDescription, Project = CurrentProject, ProjectId = CurrentProject.Id, Author = projectUser, AuthorId = projectUser.Id, Status = CurrentTicket.Status, Priority = Priority.Low, DateSubmitted = DateTime.Now });
            //Navigator.Navigate(ViewType.ProjectDetailsPage);
        }
    }
}

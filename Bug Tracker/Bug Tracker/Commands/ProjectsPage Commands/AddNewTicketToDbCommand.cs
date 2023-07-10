﻿using Bug_Tracker.State;
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
        private readonly User CurrentUser;
        public INavigator Navigator{ get; }
        private readonly IProjectContainer ProjectContainer;
        private readonly IDataService<ProjectUser> ProjectUserService;
        private readonly IDataService<Ticket> TicketService;
        private readonly CreateTicketViewModel CreateTicketViewModel;

        private Project CurrentProject { get => ProjectContainer.CurrentProject; }
        private Ticket CurrentTicket { get => ProjectContainer.CurrentTicket; }

        public AddNewTicketToDbCommand(User currentUser, INavigator navigator, IProjectContainer projectContainer, IDataService<ProjectUser> projectUserService, IDataService<Ticket> ticketService, CreateTicketViewModel createTicketViewModel)
        {
            CurrentUser = currentUser;
            Navigator = navigator;
            ProjectContainer = projectContainer;
            ProjectUserService = projectUserService;
            TicketService = ticketService;
            CreateTicketViewModel = createTicketViewModel;
        }

        public async override void Execute(object parameter)
        {
            ProjectUser projectUser = CurrentUser.ProjectUsers.FirstOrDefault(pu => pu.User.Id == CurrentUser.Id);
            ProjectContainer.CurrentTicket = await TicketService.Create(new Ticket { Title = CreateTicketViewModel.TicketTitle, Description = CreateTicketViewModel.TicketDescription, ProjectId = CurrentProject.Id, AuthorId = projectUser.Id, Status = CurrentTicket.Status, Priority = Priority.Low, DateSubmitted = DateTime.Now });
            Navigator.Navigate(ViewType.ProjectDetailsPage);
                           
        }
    }
}

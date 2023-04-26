// See https://aka.ms/new-console-template for more information
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using BugTracker.EntityFramework;
using BugTracker.EntityFramework.Services;
using Microsoft.AspNet.Identity;
using System.Reflection.Metadata;


IPasswordHasher passwordHasher= new PasswordHasher();
IUserService userDataService = new UserDataService(new BugTrackerDbContextFactory());
IDataService<Project> projectDataService = new GenericDataService<Project>(new BugTrackerDbContextFactory());
IDataService<Ticket> ticketDataService = new GenericDataService<Ticket>(new BugTrackerDbContextFactory());
IDataService<ProjectUser> projectUserDataService = new GenericDataService<ProjectUser>(new BugTrackerDbContextFactory());

IAuthenticationService authenticationService = new AuthenticationService(userDataService, passwordHasher);

//put code below
//await projectUserDataService.Delete(4);
//await projectDataService.Delete(4);
//await ticketDataService.Create(new Ticket { AuthorId = 1, ProjectId = 1, DateSubmitted = DateTime.Now, Description = "This is a test ticket created for testing purposes.", Priority = Priority.Low, Status = Status.Open, Title = "Test Ticket", TicketType = TicketType.BugsOrErrors});

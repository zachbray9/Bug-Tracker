// See https://aka.ms/new-console-template for more information
using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using BugTracker.EntityFramework;
using BugTracker.EntityFramework.Services;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

//BugTrackerDbContextFactory bugTrackerDbContextFactory = new BugTrackerDbContextFactory();
//IPasswordHasher passwordHasher= new PasswordHasher();
//IUserService userDataService = new UserDataService(new BugTrackerDbContextFactory());
//IDataService<Project> projectDataService = new GenericDataService<Project>(new BugTrackerDbContextFactory());
//IDataService<Ticket> ticketDataService = new GenericDataService<Ticket>(new BugTrackerDbContextFactory());
//IDataService<ProjectUser> projectUserDataService = new GenericDataService<ProjectUser>(new BugTrackerDbContextFactory());

BugTrackerDbContextFactory bugTrackerDbContextFactory = new BugTrackerDbContextFactory();
BugTrackerDbContext DbContext = bugTrackerDbContextFactory.CreateDbContext();
IPasswordHasher passwordHasher = new PasswordHasher();
IUserService userDataService = new UserDataService(DbContext);
IDataService<Project> projectDataService = new GenericDataService<Project>(DbContext);
IDataService<Ticket> ticketDataService = new GenericDataService<Ticket>(DbContext);
IDataService<ProjectUser> projectUserDataService = new GenericDataService<ProjectUser>(DbContext);
IDataService<Comment> commentDataService = new GenericDataService<Comment>(DbContext);

IAuthenticationService authenticationService = new AuthenticationService(userDataService, passwordHasher);

//put code below
await projectDataService.Delete(2);
await projectDataService.Delete(3);
await projectDataService.Delete(4);
await projectDataService.Delete(5);
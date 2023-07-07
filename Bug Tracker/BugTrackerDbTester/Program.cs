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

BugTrackerDbContextFactory bugTrackerDbContextFactory = new BugTrackerDbContextFactory();
IPasswordHasher passwordHasher= new PasswordHasher();
IUserService userDataService = new UserDataService(new BugTrackerDbContextFactory());
IDataService<Project> projectDataService = new GenericDataService<Project>(new BugTrackerDbContextFactory());
IDataService<Ticket> ticketDataService = new GenericDataService<Ticket>(new BugTrackerDbContextFactory());
IDataService<ProjectUser> projectUserDataService = new GenericDataService<ProjectUser>(new BugTrackerDbContextFactory());

IAuthenticationService authenticationService = new AuthenticationService(userDataService, passwordHasher);

//put code below

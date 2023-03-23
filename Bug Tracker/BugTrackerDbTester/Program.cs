// See https://aka.ms/new-console-template for more information
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using BugTracker.EntityFramework;
using BugTracker.EntityFramework.Services;
using Microsoft.AspNet.Identity;
using System.Reflection.Metadata;


IPasswordHasher passwordHasher= new PasswordHasher();
IUserService userDataService = new UserDataService(new BugTrackerDbContextFactory());


IAuthenticationService authenticationService = new AuthenticationService(userDataService, passwordHasher);

//put code below

// See https://aka.ms/new-console-template for more information
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using BugTracker.EntityFramework;
using BugTracker.EntityFramework.Services;
using System.Reflection.Metadata;

IDataService<User> userService = new GenericDataService<User>(new BugTrackerDbContextFactory());


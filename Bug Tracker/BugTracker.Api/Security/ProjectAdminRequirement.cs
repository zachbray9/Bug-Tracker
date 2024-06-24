using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BugTracker.Api.Security
{
    public class ProjectAdminRequirement : IAuthorizationRequirement
    {
    }

    public class ProjectAdminRequirementHandler : AuthorizationHandler<ProjectAdminRequirement>
    {
        private readonly BugTrackerDbContext DbContext;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public ProjectAdminRequirementHandler(BugTrackerDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            DbContext = dbContext;
            HttpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectAdminRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Task.CompletedTask;

            var projectId = Guid.Parse(HttpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var projectUser = DbContext.ProjectUsers.FindAsync(userId, projectId).Result;

            if (projectUser == null)
                return Task.CompletedTask;
            

            if (projectUser.Role == Domain.Enumerables.ProjectRole.Owner || projectUser.Role == Domain.Enumerables.ProjectRole.Administrator)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

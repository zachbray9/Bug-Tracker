using BugTracker.Api.Attributes;
using BugTracker.Domain.Models.Auth;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, BugTrackerDbContext dbContext)
        {
            var endpoint = context.GetEndpoint();
            var authorizeAttribute = endpoint?.Metadata.GetMetadata<CheckAuthorization>();

            //if the endpoint does not have the authorize attribute (the endpoint isn't private) then skip authorization
            if (authorizeAttribute == null)
            {
                await _next(context);
                return;
            }

            //checks if header contains a cookie with the session id
            string? sessionId = context.Request.Cookies["AgileSessionId"];
            if (sessionId == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            //checks if the database contains a session with the sessionId from the cookie
            AgileSession? session = await dbContext.Sessions.FirstOrDefaultAsync(s => s.Id.ToString() == sessionId);
            if (session == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            //checks if the session in the database has expired
            if(session.ExpirationDate < DateTime.UtcNow)
            {
                dbContext.Sessions.Remove(session);
                await dbContext.SaveChangesAsync();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            //successfully authenticates the user
            await _next(context);
            return;
        }
    }
}

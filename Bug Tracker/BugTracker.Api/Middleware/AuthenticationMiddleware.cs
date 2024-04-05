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

            var sessionId = context.Request.Cookies["sessionId"];
            if (!string.IsNullOrEmpty(sessionId))
            {
                // Check if session exists in the database
                AgileSession? session = await dbContext.Sessions.FirstOrDefaultAsync(s => s.Id.ToString() == sessionId);

                if (session != null && session.ExpirationDate > DateTime.Now)
                {
                    // Authentication successful
                    await _next(context);
                    return;
                }
            }

            // Authentication failed
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}

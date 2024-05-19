using BugTracker.Domain.Models;
using BugTracker.EntityFramework;

namespace BugTracker.Api.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
            })
                .AddEntityFrameworkStores<BugTrackerDbContext>();

            services.AddAuthentication();

            return services;
        }
    }
}

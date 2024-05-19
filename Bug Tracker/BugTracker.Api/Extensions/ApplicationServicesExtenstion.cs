using BugTracker.Api.Services.SessionServices;
using BugTracker.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api.Extensions
{
    public static class ApplicationServicesExtenstion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            string? ConnectionString = config["ConnectionString"];
            if(string.IsNullOrEmpty(ConnectionString))
            {
                throw new InvalidOperationException("Connection string is null or empty.");
            }
           

            services.AddDbContext<BugTrackerDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
                //options.UseInMemoryDatabase("BugTrackerTestDb");
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<SessionDbService>();
            services.AddAutoMapper(typeof(Program));

            return services;
        }
    }
}

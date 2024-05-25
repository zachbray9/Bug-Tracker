using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api.Extensions
{
    public static class ApplicationServicesExtension
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

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"); //must change url for production
                });
            });

            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Program));

            return services;
        }
    }
}

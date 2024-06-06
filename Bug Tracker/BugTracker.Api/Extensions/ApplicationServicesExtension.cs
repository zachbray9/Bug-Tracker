using Azure.Storage.Blobs;
using BugTracker.Api.Services.StorageServices;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

namespace BugTracker.Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            string? ConnectionString = config["ConnectionString"];
            string? AzureBlobConnectionString = config["AzureBlobConnectionString"];

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

            services.AddSingleton(x => new BlobServiceClient(config["AzureBlobConnectionString"]));
            services.AddScoped<BlobStorageService>();

            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Program));

            return services;
        }
    }
}

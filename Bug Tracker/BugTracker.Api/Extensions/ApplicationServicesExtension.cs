using Azure.Storage.Blobs;
using BugTracker.Api.Services.EntityServices;
using BugTracker.Api.Services.StorageServices;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            string? ConnectionString = config.GetConnectionString("DefaultConnection");
            string? AzureBlobConnectionString = config.GetConnectionString("AzureBlobStorageConnectionString");

            if(string.IsNullOrEmpty(ConnectionString))
            {
                throw new InvalidOperationException("Connection string is null or empty.");
            }

            if(string.IsNullOrEmpty(AzureBlobConnectionString))
            {
                throw new InvalidOperationException("Azure Blob Storage connection string is null or empty.");
            }
           
            services.AddDbContext<BugTrackerDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithExposedHeaders("WWW-Authenticate")
                    .WithOrigins("http://localhost:5173"); //must change url for production
                });
            });

            services.AddSignalR();
            services.AddSingleton(x => new BlobServiceClient(AzureBlobConnectionString));
            services.AddScoped<BlobStorageService>();
            services.AddScoped<CommentService>();

            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Program));

            return services;
        }
    }
}

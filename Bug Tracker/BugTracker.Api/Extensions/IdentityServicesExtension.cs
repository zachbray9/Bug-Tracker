using BugTracker.Api.Security;
using BugTracker.Api.Services.TokenServices;
using BugTracker.Domain.Models;
using BugTracker.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BugTracker.Api.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<BugTrackerDbContext>();

            services.AddScoped<AuthTokenService>();

            var jwtAuthTokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtAuthTokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = jwtAuthTokenKey,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IsProjectAdmin", policy =>
                {
                    policy.Requirements.Add(new ProjectAdminRequirement());
                });
            });

            services.AddTransient<IAuthorizationHandler, ProjectAdminRequirementHandler>();

            return services;
        }
    }
}

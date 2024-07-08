using BugTracker.Api.Extensions;
using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Text.Json.Serialization;
using BugTracker.Api.Helpers;
using BugTracker.Domain.Enumerables;
using BugTracker.Api.SignalR;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("AgileProKeyVaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.

builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(options => {
    options.JsonSerializerOptions.Converters.Add(new EnumToDisplayNameConverter<Status>());
    options.JsonSerializerOptions.Converters.Add(new EnumToDisplayNameConverter<Priority>());
    options.JsonSerializerOptions.Converters.Add(new EnumToDisplayNameConverter<ProjectRole>());
});

builder.Services.AddApplicationServices(builder.Configuration);           //extension method that adds application services
builder.Services.AddIdentityServices(builder.Configuration);              //extension method that adds microsoft identity services
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();

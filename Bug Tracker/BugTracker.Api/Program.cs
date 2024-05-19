using BugTracker.Api.Middleware;
using BugTracker.Api.Extensions;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("AgileProKeyVaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);           //extension method that adds application services
builder.Services.AddIdentityServices(builder.Configuration);              //extension method that adds microsoft identity services
builder.Services.AddControllers();
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

app.UseMiddleware<AuthenticationMiddleware>();

app.MapControllers();

app.Run();

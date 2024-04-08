using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity;
using BugTracker.Api.Middleware;
using BugTracker.Api.Services.SessionServices;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("AgileProKeyVaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

string ConnectionString = builder.Configuration["ConnectionString"];

// Add services to the container.
builder.Services.AddDbContext<BugTrackerDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
    //options.UseInMemoryDatabase("BugTrackerTestDb");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<SessionDbService>();
builder.Services.AddAutoMapper(typeof(Program));

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

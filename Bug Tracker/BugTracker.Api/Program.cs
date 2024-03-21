using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Microsoft.AspNet.Identity;
using BugTracker.Api.Services.TokenGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("AgileProKeyVaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

string ConnectionString = builder.Configuration["ConnectionString"];
string JwtAuthenticationKey = builder.Configuration["JwtAuthenticationKey"];

// Add services to the container.
builder.Services.AddDbContext<BugTrackerDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
    //options.UseInMemoryDatabase("BugTrackerTestDb");
});

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<AccessTokenGenerator>(new AccessTokenGenerator(JwtAuthenticationKey));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters() {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtAuthenticationKey)),
        ValidIssuer = "https://agileproapi.azurewebsites.net",
        ValidAudience = "https://agilepro.azurewebsites.net",
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true
    };
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

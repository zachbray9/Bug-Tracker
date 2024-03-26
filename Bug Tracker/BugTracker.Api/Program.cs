using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Microsoft.AspNet.Identity;
using BugTracker.Api.Services.TokenGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BugTracker.Api.Services.TokenValidators;
using BugTracker.Api.Services.TokenDbServices;
using BugTracker.Api.Services.Authenticators;
using BugTracker.Api.Models.Config;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("AgileProKeyVaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

string ConnectionString = builder.Configuration["ConnectionString"];
string JwtAccessTokenKey = builder.Configuration["JwtAccessTokenKey"];
string JwtRefreshTokenKey = builder.Configuration["JwtRefreshTokenKey"];

// Add services to the container.
builder.Services.AddDbContext<BugTrackerDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
    //options.UseInMemoryDatabase("BugTrackerTestDb");
});

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<TokenGenerator>();
builder.Services.AddSingleton<AccessTokenGenerator>();
builder.Services.AddSingleton<RefreshTokenGenerator>();
builder.Services.AddSingleton<RefreshTokenValidator>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<Authenticator>();
builder.Services.AddSingleton(new AuthenticationConfiguration(JwtAccessTokenKey, JwtRefreshTokenKey));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters() {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtAccessTokenKey)),
        ValidIssuer = "https://agileproapi.azurewebsites.net",
        ValidAudience = "https://agilepro.azurewebsites.net",
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero
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

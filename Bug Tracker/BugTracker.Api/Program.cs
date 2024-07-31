using BugTracker.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using BugTracker.Api.Helpers;
using BugTracker.Domain.Enumerables;
using BugTracker.Api.SignalR;
using BugTracker.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
})
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new EnumToDisplayNameConverter<Status>());
        options.SerializerSettings.Converters.Add(new EnumToDisplayNameConverter<Priority>());
        options.SerializerSettings.Converters.Add(new EnumToDisplayNameConverter<ProjectRole>());
    });

builder.Services.AddApplicationServices(builder.Configuration);           //extension method that adds application services
builder.Services.AddIdentityServices(builder.Configuration);              //extension method that adds microsoft identity services
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseXContentTypeOptions();
app.UseReferrerPolicy(opt => opt.NoReferrer());
app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
app.UseXfo(opt => opt.Deny());
app.UseCsp(opt => opt
    .BlockAllMixedContent()
    .StyleSources(s => s.Self().CustomSources("https://fonts.googleapis.com", "sha256-47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU=", "sha256-Q9MUdYBtYzn5frLpoNRLdFYW76cJ4ok2SmIKzTFq57Q=", "sha256-GNF74DLkXb0fH3ILHgILFjk1ozCF3SNXQ5mQb7WLu/Y=", "sha256-nzTgYzXYDNe6BAHiiI7NNlfK8n/auuOAhh2t92YvuXo="))
    .FontSources(s => s.Self().CustomSources("https://fonts.gstatic.com"))
    .FormActions(s => s.Self())
    .FrameAncestors(s => s.Self())
    .ImageSources(s => s.Self().CustomSources("blob:", "https://agileproblobstorage.blob.core.windows.net"))
    .ScriptSources(s => s.Self())
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
        await next.Invoke();
    });
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.MapHub<ChatHub>("/chat");
app.MapFallbackToController("Index", "Fallback");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<BugTrackerDbContext>();
    context.Database.Migrate();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger>();
    logger.LogError(ex, "An error occurred during migration.");
}

app.Run();

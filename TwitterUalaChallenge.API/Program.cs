using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using TwitterUalaChallenge.API.Bootstrap;
using TwitterUalaChallenge.API.Bootstrap.Providers;
using TwitterUalaChallenge.Application.Bootstrap;
using TwitterUalaChallenge.Infrastructure.Bootstrap;
using TwitterUalaChallenge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddObservability();

builder.Services.AddAPIServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.AddAPIModules();
        containerBuilder.AddInfrastructureModules();
        containerBuilder.AddCoreApplicationModules();
    });

var app = builder.Build();

app.UsePathBase("/{{API-PATH}}");
app.UseRouting();

ApplyMigrations(app);

app.UseAPIMiddlewares();
app.UseObservability();

app.Run();

static void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<TwitterUalaChallengeDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
}
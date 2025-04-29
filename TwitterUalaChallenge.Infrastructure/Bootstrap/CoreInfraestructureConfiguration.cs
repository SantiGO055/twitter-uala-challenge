using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using TwitterUalaChallenge.Infrastructure.Bootstrap.Modules;
using TwitterUalaChallenge.Infrastructure.Persistence;

namespace TwitterUalaChallenge.Infrastructure.Bootstrap;

public static class CoreInfrastructureConfiguration
{
    public static IServiceCollection AddCoreInfrastructureServices(this IServiceCollection services)
    {
        services.AddOptions();
        return services;
    }

    public static void AddCoreInfrastructureModules(this ContainerBuilder builder, Assembly assembly)
    {
    }

    public static void AddDatabaseModule<TDbContext>(this ContainerBuilder builder, Assembly assembly)
        where TDbContext : BaseDbContext
    {
        builder.RegisterModule(new DataBaseModule<TDbContext>(assembly));
    }
}
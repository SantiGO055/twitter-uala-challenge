using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using TwitterUalaChallenge.Application.Bootstrap.Modules;
using TwitterUalaChallenge.Application.Bootstrap.Providers;

namespace TwitterUalaChallenge.Application.Bootstrap;

public static class CoreApplicationConfiguration
{
    public static IServiceCollection AddCoreApplicationServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(assembly); });

        services.AddFluentValidation(assembly);

        return services;
    }

    public static void AddCoreApplicationModules(this ContainerBuilder builder, Assembly assembly)
    {
        builder.RegisterModule(new MediatRModule(assembly));
        builder.RegisterModule(new ServicesModule(assembly));
    }
}
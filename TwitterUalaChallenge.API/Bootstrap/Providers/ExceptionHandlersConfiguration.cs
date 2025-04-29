using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TwitterUalaChallenge.Contracts.Core.Api.ExceptionHandler;

namespace TwitterUalaChallenge.API.Bootstrap.Providers;

public static class ExceptionHandlersConfiguration
{
    public static IServiceCollection AddExceptionHandlers(
        this IServiceCollection services,
        List<Assembly> assemblies = null)
    {
        assemblies ??= new List<Assembly>();
        assemblies.Add(Assembly.GetExecutingAssembly());

        foreach (var assemblyItem in assemblies)
        {
            services.RegisterExceptionHandlers(assemblyItem);
        }

        return services;
    }

    private static IServiceCollection RegisterExceptionHandlers(this IServiceCollection services, Assembly assembly)
    {
        var exceptionHandlerTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            typeof(IBaseExceptionHandler).IsAssignableFrom(i.GetGenericTypeDefinition()))
                .Select(i => new { ImplementationType = t, InterfaceType = i }));

        foreach (var handler in exceptionHandlerTypes)
        {
            services.AddScoped(typeof(IBaseExceptionHandler), handler.ImplementationType);
        }

        return services;
    }
}
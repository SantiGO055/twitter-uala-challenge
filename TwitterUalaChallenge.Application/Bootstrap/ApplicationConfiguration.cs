using Autofac;
using TwitterUalaChallenge.Application.Bootstrap;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TwitterUalaChallenge.Application.Bootstrap
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddCoreApplicationServices(assembly);

            return services;
        }

        public static void AddCoreApplicationModules(this ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.AddCoreApplicationModules(assembly);
        }
    }
}
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TwitterUalaChallenge.Infrastructure.Persistence;

namespace TwitterUalaChallenge.Infrastructure.Bootstrap
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddCoreInfrastructureServices();

            return services;
        }

        public static void AddInfrastructureModules(this ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            builder.AddCoreInfrastructureModules(assembly);
            builder.AddDatabaseModule<TwitterUalaChallengeDbContext>(assembly);
        }
    }
}
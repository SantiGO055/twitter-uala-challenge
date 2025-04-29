using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using TwitterUalaChallenge.API.Bootstrap.Providers;

namespace TwitterUalaChallenge.API.Bootstrap
{
    public static class APIConfiguration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddCoreAPIServices(configuration, assembly);
            services.AddExceptionHandlers();

            return services;
        }

        public static void AddAPIModules(this ContainerBuilder builder)
        {
        }

        public static IApplicationBuilder UseAPIMiddlewares(this IApplicationBuilder app)
        {
            var assembly = Assembly.GetExecutingAssembly();
            app.UseCoreAPIMiddlewares(assembly);

            return app;
        }
    }
}
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterUalaChallenge.API.Bootstrap.Providers;
using TwitterUalaChallenge.API.ExceptionHandlers;
using TwitterUalaChallenge.API.Middlewares;
using TwitterUalaChallenge.Common.Constants;

namespace TwitterUalaChallenge.API.Bootstrap;

public static class CoreAPIConfiguration
{
    public static IServiceCollection AddCoreAPIServices(this IServiceCollection services, IConfiguration configuration,
        Assembly assembly)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHeaderPropagation();
        CorsConfiguration.AddCors(services);
        services.AddVersioning();
        services.AddCommonSwaggerConfigurations();
        services.AddExceptionHandler<ValidationCustomExceptionHandler>();
        services.AddExceptionHandler<BusinessExceptionHandler>();
        services.AddExceptionHandler<ClientExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static IApplicationBuilder UseCoreAPIMiddlewares(this IApplicationBuilder app, Assembly assembly)
    {
        app.UseRouting();
        // app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseSwagger(assembly);
        app.UseCors(GlobalConstants.CorsPolicyName);
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHeaderPropagation();
        app.UseExceptionHandler();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        return app;
    }
}
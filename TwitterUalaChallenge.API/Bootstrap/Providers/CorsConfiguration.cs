using System;
using Microsoft.Extensions.DependencyInjection;
using TwitterUalaChallenge.Common.Constants;

namespace TwitterUalaChallenge.API.Bootstrap.Providers;

public static class CorsConfiguration
{
    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        var allowedOrigins = Environment.GetEnvironmentVariable(ConfigurationKeys.AllowedOrigins) ?? "*";
        var allowedMethods = Environment.GetEnvironmentVariable(ConfigurationKeys.AllowedMethods) ?? "GET,POST,OPTIONS";

        services.AddCors(options =>
        {
            options.AddPolicy(name: GlobalConstants.CorsPolicyName,
                builder =>
                {
                    builder
                        .WithOrigins(allowedOrigins.Split(","))
                        .WithMethods(allowedMethods.Split(","))
                        .AllowAnyHeader();
                });
        });

        return services;
    }
}
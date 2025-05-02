using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TwitterUalaChallenge.API.Middlewares;
using TwitterUalaChallenge.Common.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TwitterUalaChallenge.API.Bootstrap.Providers;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddCommonSwaggerConfigurations(this IServiceCollection services)
    {
        services.AddSwaggerGen(delegate(SwaggerGenOptions options)
        {
            options.OperationFilter<SwaggerDefaultValues>();
            string path = Assembly.GetEntryAssembly().GetName().Name + ".xml";
            string text = Path.Combine(AppContext.BaseDirectory, path);
            if (File.Exists(text))
            {
                options.IncludeXmlComments(text);
            }
            
        });
        return services;
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, Assembly assembly)
    {
        var swaggerEnabled = Convert.ToBoolean(
            Environment.GetEnvironmentVariable(
                ConfigurationKeys.SwaggerEnabled));

        if (!swaggerEnabled)
            return app;

        app.UseSwagger();
        app.UseSwaggerUI(op =>
        {
            GetApiVersions(assembly).ForEach(apiVersion =>
            {
                op.SwaggerEndpoint(
                    $"/swagger/{apiVersion}/swagger.json",
                    $"{assembly.GetName().Name} - {apiVersion}");
            });
        });

        return app;
    }

    private static List<string> GetApiVersions(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<ApiVersionAttribute>() != null)
            .Select(t => $"v{t.GetCustomAttribute<ApiVersionAttribute>().Versions[0]}")
            .Distinct()
            .OrderBy(version => int.Parse(version[1..]))
            .ToList();
    }
}
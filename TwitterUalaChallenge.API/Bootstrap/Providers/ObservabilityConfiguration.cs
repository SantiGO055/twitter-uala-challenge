using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace TwitterUalaChallenge.API.Bootstrap.Providers
{
    public static class ObservabilityConfiguration
    {
        public static WebApplicationBuilder AddObservability(this WebApplicationBuilder builder)
        {
            var mimimumLogLevel = Environment.GetEnvironmentVariable("MINIMUM_LOG_LEVEL");

            _ = Enum.TryParse(mimimumLogLevel, out LogLevel loglevel);

            builder.Logging.ClearProviders();
            builder.Services.AddHttpContextAccessor();

            return builder;
        }

        public static IApplicationBuilder UseObservability(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
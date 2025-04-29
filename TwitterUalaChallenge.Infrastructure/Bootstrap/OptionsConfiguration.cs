using Microsoft.Extensions.DependencyInjection;
using TwitterUalaChallenge.Common.Constants;
using TwitterUalaChallenge.Common.Options;

namespace TwitterUalaChallenge.Infrastructure.Bootstrap;

internal static class OptionsConfiguration
{
    internal static IServiceCollection AddOptions(this IServiceCollection services)
    {
        services.AddExceptionHandlingOptions();

        return services;
    }

    private static IServiceCollection AddExceptionHandlingOptions(this IServiceCollection services)
    {
        services.AddOptions<ExceptionHandlingOptions>().Configure(opt =>
        {
            opt.ShowExceptionDetails = bool.Parse(
                Environment.GetEnvironmentVariable(
                    ConfigurationKeys.ShowExceptionDetail) ?? string.Empty);
        });

        return services;
    }
}
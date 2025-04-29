using System.Globalization;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TwitterUalaChallenge.Application.Bootstrap.Providers;

internal static class FluentValidationConfiguration
{
    internal static IServiceCollection AddFluentValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");

        ValidatorOptions.Global.PropertyNameResolver = (_, memberInfo, _) => { return memberInfo.Name; };

        return services;
    }
}
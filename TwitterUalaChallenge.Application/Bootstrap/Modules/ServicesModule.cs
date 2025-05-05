using System.Reflection;
using Autofac;

namespace TwitterUalaChallenge.Application.Bootstrap.Modules;

internal class ServicesModule : Autofac.Module
{
    private readonly Assembly _assembly;

    public ServicesModule(Assembly assembly)
    {
        _assembly = assembly;
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        RegisterServices(builder);
    }

    private void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(_assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .OnRegistered(e => Console.WriteLine($"Registered: {e.ComponentRegistration.Activator.LimitType.Name}"));
    }
}
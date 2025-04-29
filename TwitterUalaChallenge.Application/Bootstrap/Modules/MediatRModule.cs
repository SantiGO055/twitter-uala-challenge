using System.Reflection;
using Autofac;
using MediatR;
using TwitterUalaChallenge.Application.Behaviors;

namespace TwitterUalaChallenge.Application.Bootstrap.Modules;

internal class MediatRModule : Autofac.Module
{
    private readonly Assembly _assembly;

    public MediatRModule(Assembly assembly)
    {
        _assembly = assembly;
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        var assembly = Assembly.GetExecutingAssembly();

        RegisterMediatR(builder);
        RegisterBehaviors(builder);
    }

    private void RegisterMediatR(ContainerBuilder builder)
    {
        builder.RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(_assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(_assembly)
            .AsClosedTypesOf(typeof(INotificationHandler<>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }

    private void RegisterBehaviors(
        ContainerBuilder builder)
    {
        var behaviors = new List<Type>
        {
            typeof(TransactionalBehavior<,>),
            typeof(ValidationBehavior<,>)
        };

        foreach (var behavior in behaviors)
        {
            builder.RegisterGeneric(behavior)
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerLifetimeScope();
        }
    }
}
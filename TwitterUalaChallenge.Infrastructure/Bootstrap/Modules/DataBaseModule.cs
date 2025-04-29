using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using TwitterUalaChallenge.Common.Constants;
using TwitterUalaChallenge.Contracts.Core.Infraestructure;
using TwitterUalaChallenge.Infrastructure.Persistence;

namespace TwitterUalaChallenge.Infrastructure.Bootstrap.Modules;

internal class DataBaseModule<TDbContext>(Assembly assembly) : Autofac.Module
    where TDbContext : BaseDbContext
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        var assembly = Assembly.GetExecutingAssembly();

        RegisterDatabase(builder);
        RegisterRepositories(builder, assembly);
        RegisterUnitOfWork(builder);
    }

    private static void RegisterDatabase(ContainerBuilder builder)
    {
        var pgHost = Environment.GetEnvironmentVariable(ConfigurationKeys.PgHost);
        var pgPort = Environment.GetEnvironmentVariable(ConfigurationKeys.PgPort);
        var pgDatabase = Environment.GetEnvironmentVariable(ConfigurationKeys.PgDatabase);
        var pgPassword = Environment.GetEnvironmentVariable(ConfigurationKeys.PgPassword);
        var pgUser = Environment.GetEnvironmentVariable(ConfigurationKeys.PgUser);

        var connectionString =
            $"User Id={pgUser};Host={pgHost}; Port={pgPort}; Database={pgDatabase}; Password={pgPassword}; Include Error Detail = true;";

        builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
                optionsBuilder.UseNpgsql(connectionString)
                    .UseSnakeCaseNamingConvention();

                return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
            })
            .AsSelf()
            .InstancePerLifetimeScope();

        builder.Register(x => { return new ReadOnlyQuery(connectionString); }).As<IReadOnlyQuery>()
            .InstancePerLifetimeScope();
    }

    private void RegisterRepositories(ContainerBuilder builder, Assembly assembly1)
    {
        builder.RegisterAssemblyTypes(assembly, assembly1)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }

    private void RegisterUnitOfWork(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork<TDbContext>>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();


        builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.Name.EndsWith("UnitOfWork"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
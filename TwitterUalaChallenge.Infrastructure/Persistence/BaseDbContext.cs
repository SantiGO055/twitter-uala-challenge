using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TwitterUalaChallenge.Infrastructure.Persistence;

public abstract class BaseDbContext(DbContextOptions options) : DbContext(options)
{
    protected abstract string DefaultSchemaName { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchemaName);

        base.OnModelCreating(modelBuilder);
        ConfigureEntities(modelBuilder);
    }

    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        var entityTypeConfigurationTypes = GetType().Assembly.GetTypes()
            .Where(type => type.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            .ToList();

        var genericMethodDefinition = GetType().BaseType
            .GetMethod(nameof(ApplyEntityTypeConfiguration), BindingFlags.Instance | BindingFlags.NonPublic);

        foreach (var entityTypeConfigurationType in entityTypeConfigurationTypes)
        {
            var genericMethod = genericMethodDefinition
                .MakeGenericMethod([
                    entityTypeConfigurationType,
                    entityTypeConfigurationType.GetInterfaces()[0].GetGenericArguments()[0]
                ]);

            genericMethod.Invoke(this, [modelBuilder]);
        }
    }

    private void ApplyEntityTypeConfiguration<TEntityTypeConfiguration, TEntity>(ModelBuilder modelBuilder)
        where TEntity : class
        where TEntityTypeConfiguration : IEntityTypeConfiguration<TEntity>, new()
    {
        modelBuilder.ApplyConfiguration(new TEntityTypeConfiguration());
    }
}
using TwitterUalaChallenge.Contracts.Core.Infraestructure;

namespace TwitterUalaChallenge.Infrastructure.Persistence;

public class UnitOfWork<TContext>(TContext dbContext) : IUnitOfWork
    where TContext : BaseDbContext
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
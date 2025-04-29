namespace TwitterUalaChallenge.Contracts.Core.Infraestructure;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
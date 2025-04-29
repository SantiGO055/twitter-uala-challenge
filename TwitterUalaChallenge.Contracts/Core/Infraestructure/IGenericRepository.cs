using System.Linq.Expressions;

namespace TwitterUalaChallenge.Contracts.Core.Infraestructure;

public interface IGenericRepository<TEntity>
    where TEntity : class
{
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync();
}
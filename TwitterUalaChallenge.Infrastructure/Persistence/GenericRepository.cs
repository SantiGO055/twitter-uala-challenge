using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TwitterUalaChallenge.Contracts.Core.Infraestructure;

namespace TwitterUalaChallenge.Infrastructure.Persistence;

public class GenericRepository<TEntity, TContext>(TContext context) : IGenericRepository<TEntity>
    where TEntity : class
    where TContext : BaseDbContext
{
    protected readonly TContext _context = context;
    private readonly DbSet<TEntity> _entities = context.Set<TEntity>();

    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.SingleOrDefaultAsync(predicate);
    }

    public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        if (predicate == null)
            return await _entities.ToListAsync();

        return await _entities.Where(predicate).ToListAsync();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _entities.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
using TwitterUalaChallenge.Contracts.Core.Infraestructure;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Contracts.Repositories;

public interface ITweetRepository: IGenericRepository<Tweet>
{
    Task<IEnumerable<Tweet>> GetUserTimelineAsync(Guid userId, int page, int limit);
}
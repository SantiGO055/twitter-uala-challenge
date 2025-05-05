using TwitterUalaChallenge.Contracts.Core.Infraestructure;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Contracts.Repositories;

public interface IUserRepository: IGenericRepository<User>
{
    public Task<IEnumerable<User>> GetPagedAsync(int page, int limit);
}
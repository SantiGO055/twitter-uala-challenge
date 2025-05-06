using TwitterUalaChallenge.Contracts.Core.Infraestructure;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Contracts.Repositories;

public interface IFollowRepository: IGenericRepository<Follow>
{
    public Task<Follow> GetFollow(Guid followerId, Guid userToFollowId);
}
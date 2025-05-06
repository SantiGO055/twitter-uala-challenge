using Microsoft.EntityFrameworkCore;
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Infrastructure.Persistence.Repositories;

public class FollowRepository(TwitterUalaChallengeDbContext dbContext) :
    GenericRepository<Follow, TwitterUalaChallengeDbContext>(dbContext),
    IFollowRepository
{
    public Task<Follow> GetFollow(Guid followerId, Guid userToFollowId)
    {
        return dbContext.Follows
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == userToFollowId);
    }
}
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Infrastructure.Persistence.Repositories;

public class TweetRepository (TwitterUalaChallengeDbContext dbContext) :
    GenericRepository<Tweet, TwitterUalaChallengeDbContext>(dbContext),
    ITweetRepository
{

}
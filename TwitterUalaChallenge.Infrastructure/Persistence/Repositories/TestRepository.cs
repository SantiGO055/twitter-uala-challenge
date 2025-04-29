using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Infrastructure.Persistence.Repositories
{
    public class TestRepository(TwitterUalaChallengeDbContext dbContext) :
        GenericRepository<Test, TwitterUalaChallengeDbContext>(dbContext),
        ITestRepository
    {
    }
}
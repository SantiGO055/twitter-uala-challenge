using Microsoft.EntityFrameworkCore;
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Infrastructure.Persistence.Repositories;

public class UserRepository(TwitterUalaChallengeDbContext dbContext) :
    GenericRepository<User, TwitterUalaChallengeDbContext>(dbContext),
    IUserRepository
{
    private readonly TwitterUalaChallengeDbContext _dbContext = dbContext;

    public async Task<IEnumerable<User>> GetPagedAsync(int page, int limit)
    {
        return await _dbContext.Users
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }
}
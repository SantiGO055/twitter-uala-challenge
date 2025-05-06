using Microsoft.EntityFrameworkCore;
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Infrastructure.Persistence.Repositories;

public class TweetRepository (TwitterUalaChallengeDbContext dbContext) :
    GenericRepository<Tweet, TwitterUalaChallengeDbContext>(dbContext),
    ITweetRepository
{
    private readonly TwitterUalaChallengeDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Tweet>> GetUserTimelineAsync(Guid userId, int page, int limit)
    {

        var query = await _context.Tweets
            .Where(t => _context.Follows
                .Any(f => f.FollowerId == userId && f.FollowedId == t.UserId))
            .OrderByDescending(t => t.CreatedDate)
            .Select(t => new Tweet
            {
                TweetId = t.TweetId,
                Content = t.Content,
                CreatedDate = t.CreatedDate,
                User = t.User
            })
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return query;

    }
}
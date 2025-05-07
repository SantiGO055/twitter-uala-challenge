using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Application.Services;

public class TweetService(ITweetRepository entityTweetRepository, IUserService userService): ITweetService
{
    public async Task<Tweet> CreateContentAsync(Guid userId, string content)
    {
        var user = await userService.GetUserByIdAsync(userId);

        var tweet = new Tweet { Content = content , User = user, UserId = user.UserId};
        await entityTweetRepository.AddAsync(tweet);
        await entityTweetRepository.SaveChangesAsync();

        return tweet;
    }

    public async Task<PaginatedResponse<TimelineResponse>> GetTimelineByUserAsync(Guid userId, int page, int limit)
    {
        page = Math.Max(1, page);
        limit = Math.Clamp(limit, 1, 100);

        var tweets = await entityTweetRepository.GetUserTimelineAsync(userId, page, limit);

        var enumerable = tweets as Tweet[] ?? tweets.ToArray();
        var totalTweets = enumerable.Length;

        return new PaginatedResponse<TimelineResponse>(
            enumerable.Select(t => new TimelineResponse
            {
                TweetId = t.TweetId,
                Content = t.Content,
                CreatedDate = t.CreatedDate,
                Author = new UserResponse
                {
                    UserId = t.User.UserId,
                    UserName = t.User.UserName
                }
            }).ToList(),
            page,
            limit,
            totalTweets
        );
    }
}
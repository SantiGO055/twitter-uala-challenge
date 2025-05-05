using TwitterUalaChallenge.Application.Services.Interfaces;
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
}
using Moq;
using TwitterUalaChallenge.Application.Services;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Tests.Application.v1.Services;

public class TweetServiceTest
{
    private readonly Mock<ITweetService> _tweetServiceMock = new();
    private readonly Mock<ITweetRepository> _tweetRepositoryMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();

    [Fact]
    public async Task CreateTweet_ShouldReturnCreatedTweet()
    {
        // Arrange
        var tweet = new Tweet
        {
            TweetId = Guid.NewGuid(),
            Content = "Hello, world!",
            CreatedDate = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        };

        _tweetServiceMock.Setup(x => x.CreateContentAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(tweet);

        // Act
        var result = await _tweetServiceMock.Object.CreateContentAsync(It.IsAny<Guid>(), It.IsAny<string>());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tweet.Content, result.Content);
    }

    [Fact]
    public async Task GetTweetById_ShouldReturnTweet()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tweets = new List<Tweet>
        {
            new()
            {
                TweetId = Guid.NewGuid(),
                Content = "Tweet 1",
                CreatedDate = DateTime.UtcNow,
                User = new User { UserId = userId, UserName = "usuario1" }
            },
            new()
            {
                TweetId = Guid.NewGuid(),
                Content = "Tweet 2",
                CreatedDate = DateTime.UtcNow,
                User = new User { UserId = userId, UserName = "usuario1" }
            }
        };

        _tweetRepositoryMock.Setup(x => x.GetUserTimelineAsync(userId, 1, 10))
            .ReturnsAsync(tweets);

        var sut = new TweetService(_tweetRepositoryMock.Object, _userServiceMock.Object);

        // Act
        var result = await sut.GetTimelineByUserAsync(userId, 1, 10);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Page);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(tweets[0].Content, result.Items[0].Content);
        Assert.Equal(tweets[0].User.UserName, result.Items[0].Author.UserName);
    }

    [Fact]
    public async Task GetTimelineByUserAsync_WithInvalidPage_ShouldAdjustToMinimumPage()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tweets = new List<Tweet>();

        _tweetRepositoryMock.Setup(x => x.GetUserTimelineAsync(userId, 1, 10))
            .ReturnsAsync(tweets);

        var sut = new TweetService(_tweetRepositoryMock.Object, _userServiceMock.Object);

        // Act
        var result = await sut.GetTimelineByUserAsync(userId, -1, 10);

        // Assert
        Assert.Equal(1, result.Page);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task GetTimelineByUserAsync_WithInvalidLimit_ShouldClampLimit()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tweets = new List<Tweet>();

        _tweetRepositoryMock.Setup(x => x.GetUserTimelineAsync(userId, 1, 100))
            .ReturnsAsync(tweets);

        var sut = new TweetService(_tweetRepositoryMock.Object, _userServiceMock.Object);

        // Act
        var result = await sut.GetTimelineByUserAsync(userId, 1, 200);

        // Assert
        Assert.Equal(100, result.PageSize);
        Assert.Empty(result.Items);
    }
}
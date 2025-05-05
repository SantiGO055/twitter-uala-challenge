using Moq;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Tests.Application.v1.Services;

public class TweetServiceTest
{
    private readonly Mock<ITweetService> _userServiceMock = new();

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

        _userServiceMock.Setup(x => x.CreateContentAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(tweet);

        // Act
        var result = await _userServiceMock.Object.CreateContentAsync(It.IsAny<Guid>(), It.IsAny<string>());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tweet.Content, result.Content);
    }
}
using System.Net;
using Moq;
using TwitterUalaChallenge.Application.Services;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Tests.Application.v1.Services;

public class FollowServiceTest
{
    private readonly Mock<IFollowRepository> _followRepositoryMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly IFollowService _sut;

    public FollowServiceTest()
    {
        _sut = new FollowService(_followRepositoryMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task FollowUserAsync_ShouldFollow_WhenUsersExistAndNotFollowing()
    {
        // Arrange
        var followerId = Guid.NewGuid();
        var userToFollowId = Guid.NewGuid();

        var follower = new User { UserId = followerId, UserName = "follower" };
        var userToFollow = new User { UserId = userToFollowId, UserName = "followed" };

        _userServiceMock.Setup(x => x.GetUserByIdAsync(followerId))
            .ReturnsAsync(follower);
        _userServiceMock.Setup(x => x.GetUserByIdAsync(userToFollowId))
            .ReturnsAsync(userToFollow);
        _followRepositoryMock.Setup(x => x.GetFollow(followerId, userToFollowId))
            .ReturnsAsync((Follow)null);

        // Act
        var result = await _sut.FollowUserAsync(followerId, userToFollowId);

        // Assert
        Assert.True(result);
        _followRepositoryMock.Verify(x => x.AddAsync(It.Is<Follow>(f =>
            f.FollowerId == followerId &&
            f.FollowedId == userToFollowId)), Times.Once);
    }

    [Fact]
    public async Task FollowUserAsync_ShouldThrowException_WhenFollowingSelf()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(
            () => _sut.FollowUserAsync(userId, userId));

        Assert.Equal(ApiErrorType.FollowingSelf.ErrorMessage, exception.ApiErrorType.ErrorMessage);
        Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
    }

    [Fact]
    public async Task FollowUserAsync_ShouldThrowException_WhenAlreadyFollowing()
    {
        // Arrange
        var followerId = Guid.NewGuid();
        var userToFollowId = Guid.NewGuid();

        var follower = new User { UserId = followerId, UserName = "follower" };
        var userToFollow = new User { UserId = userToFollowId, UserName = "followed" };
        var existingFollow = new Follow
        {
            FollowerId = followerId,
            FollowedId = userToFollowId
        };

        _userServiceMock.Setup(x => x.GetUserByIdAsync(followerId))
            .ReturnsAsync(follower);
        _userServiceMock.Setup(x => x.GetUserByIdAsync(userToFollowId))
            .ReturnsAsync(userToFollow);
        _followRepositoryMock.Setup(x => x.GetFollow(followerId, userToFollowId))
            .ReturnsAsync(existingFollow);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(
            () => _sut.FollowUserAsync(followerId, userToFollowId));

        Assert.Equal(ApiErrorType.AlreadyFollowing.ErrorMessage, exception.ApiErrorType.ErrorMessage);
    }

    [Fact]
    public async Task UnfollowUserAsync_ShouldUnfollow_WhenFollowExists()
    {
        // Arrange
        var followerId = Guid.NewGuid();
        var userToUnfollowId = Guid.NewGuid();
        var existingFollow = new Follow
        {
            FollowerId = followerId,
            FollowedId = userToUnfollowId
        };

        _followRepositoryMock.Setup(x => x.GetFollow(followerId, userToUnfollowId))
            .ReturnsAsync(existingFollow);

        // Act
        var result = await _sut.UnfollowUserAsync(followerId, userToUnfollowId);

        // Assert
        Assert.True(result);
        _followRepositoryMock.Verify(x => x.Delete(existingFollow), Times.Once);
    }

    [Fact]
    public async Task UnfollowUserAsync_ShouldThrowException_WhenNotFollowing()
    {
        // Arrange
        var followerId = Guid.NewGuid();
        var userToUnfollowId = Guid.NewGuid();

        _followRepositoryMock.Setup(x => x.GetFollow(followerId, userToUnfollowId))
            .ReturnsAsync((Follow)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(
            () => _sut.UnfollowUserAsync(followerId, userToUnfollowId));

        Assert.Equal(ApiErrorType.NotFollowing.ErrorMessage, exception.ApiErrorType.ErrorMessage);
        Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
    }
}
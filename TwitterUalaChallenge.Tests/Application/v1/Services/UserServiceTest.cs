using System.Net;
using Moq;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Tests.Application.v1.Services;

public class UserServiceTest
{
    private readonly Mock<IUserService> _userServiceMock = new();


    [Fact]
    public async Task GetUserById_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedUser = new User { UserId = userId, UserName = "John Doe" };

        _userServiceMock.Setup(x => x.GetUserByIdAsync(userId))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _userServiceMock.Object.GetUserByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser.UserId, result.UserId);
        Assert.Equal(expectedUser.UserName, result.UserName);
    }

    [Fact]
    public async Task GetUserById_ShouldThrowException_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var exceptionMessage = ApiErrorType.UserNotFound.ErrorMessage;
        var errorCode = ApiErrorType.UserNotFound.ErrorCode;

        _userServiceMock.Setup(x => x.GetUserByIdAsync(userId))
            .ThrowsAsync(new BusinessException(ApiErrorType.UserNotFound, HttpStatusCode.NotFound));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _userServiceMock.Object.GetUserByIdAsync(userId));
        Assert.Equal(exceptionMessage, exception.ApiErrorType.ErrorMessage);
        Assert.Equal(errorCode, exception.ApiErrorType.ErrorCode);
    }

    [Fact]
    public async Task GetUsersAsync_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var expectedUsers = new List<User>
        {
            new() { UserId = Guid.NewGuid(), UserName = "John Doe" },
            new() { UserId = Guid.NewGuid(), UserName = "Jane Smith" }
        };

        _userServiceMock.Setup(x => x.GetUsersAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedUsers);

        // Act
        var result = await _userServiceMock.Object.GetUsersAsync(It.IsAny<int>(), It.IsAny<int>());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUsers.Count, result.Count());
    }
}
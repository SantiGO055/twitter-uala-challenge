using System.Linq.Expressions;
using System.Net;
using Moq;
using TwitterUalaChallenge.Application.Services;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Tests.Application.v1.Services;

public class UserServiceTest
{
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    private readonly IUserService _sut;

    public UserServiceTest()
    {
        _sut = new UserService(_userRepositoryMock.Object);
    }

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
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _sut.GetUserByIdAsync(userId));
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

    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser_WhenUserNameDoesNotExist()
    {
        // Arrange
        var userName = "newUser";
        var expectedUser = new User { UserId = Guid.NewGuid(), UserName = userName.ToLowerInvariant() };

        _userRepositoryMock.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<User, bool>>>()))!
            .ReturnsAsync((User)null!);

        _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>()))
            .Returns(Task.FromResult(expectedUser));

        // Act
        var result = await _sut.CreateUserAsync(userName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser.UserName, result.UserName);
        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldThrowException_WhenUserNameExists()
    {
        // Arrange
        var userName = "existingUser";
        var existingUser = new User { UserId = Guid.NewGuid(), UserName = userName.ToLowerInvariant() };

        _userRepositoryMock.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(existingUser);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(
            () => _sut.CreateUserAsync(userName));

        Assert.Equal(ApiErrorType.UserAlreadyExists.ErrorMessage, exception.ApiErrorType.ErrorMessage);
        Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
    }

    [Theory]
    [InlineData("User1")]
    [InlineData("USER1")]
    [InlineData("uSeR1")]
    public async Task CreateUserAsync_ShouldCreateUserWithLowerCaseName_RegardlessOfInputCase(string userName)
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User)null);

        // Act
        var result = await _sut.CreateUserAsync(userName);

        // Assert
        Assert.Equal(userName.ToLowerInvariant(), result.UserName);
    }
}
using System.Net;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateUser;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Application.Services;

public class UserService(IUserRepository entityUserRepository): IUserService
{
    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var user = await entityUserRepository.GetSingleAsync(x => x.UserId == id);

        return user ?? throw new BusinessException(ApiErrorType.UserNotFound, HttpStatusCode.NotFound);
    }

    public async Task<User> CreateUserAsync(string userName)
    {
        var user = new User { UserName = userName.ToLowerInvariant() };
        var userExists = await entityUserRepository.GetSingleAsync(x => x.UserName == userName.ToLowerInvariant());

        if (userExists is not null)
            throw new BusinessException(ApiErrorType.UserAlreadyExists, HttpStatusCode.BadRequest);

        await entityUserRepository.AddAsync(user);
        await entityUserRepository.SaveChangesAsync();

        return user ?? throw new BusinessException(ApiErrorType.UserAlreadyExists, HttpStatusCode.BadRequest);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int page, int limit)
    {
        var users = await entityUserRepository.GetPagedAsync(page, limit);

        return users;
    }
}
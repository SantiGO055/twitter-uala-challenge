using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Application.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> CreateUserAsync(string userName);
    Task<IEnumerable<User>> GetUsersAsync(int page, int limit);
}
namespace TwitterUalaChallenge.Application.Services.Interfaces;

public interface IFollowService
{
    Task<bool> FollowUserAsync(Guid followerId, Guid userToFollowId);
    Task<bool> UnfollowUserAsync(Guid followerId, Guid userToUnfollowId);
    // Task<bool> IsFollowingAsync(Guid followerId, Guid followedId);
    // Task<IEnumerable<UserProfileDto>> GetFollowersAsync(Guid userId);
    // Task<IEnumerable<UserProfileDto>> GetFollowingAsync(Guid userId);
}
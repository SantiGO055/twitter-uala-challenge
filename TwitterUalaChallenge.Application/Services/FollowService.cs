using System.Net;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.Errors;
using TwitterUalaChallenge.Common.Exceptions;
using TwitterUalaChallenge.Contracts.Repositories;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Application.Services;

public class FollowService(IFollowRepository entityFollowRepository, IUserService userService): IFollowService
{
    public async Task<bool> FollowUserAsync(Guid followerId, Guid userToFollowId)
    {
        if (followerId == userToFollowId)
            throw new BusinessException(ApiErrorType.FollowingSelf, HttpStatusCode.BadRequest);

        var followerUser = await userService.GetUserByIdAsync(followerId);

        var userToFollow = await userService.GetUserByIdAsync(userToFollowId);

        var alreadyFollowing = await entityFollowRepository.GetFollow(followerUser.UserId, userToFollow.UserId);

        if (alreadyFollowing is not null)
            throw new BusinessException(ApiErrorType.AlreadyFollowing, HttpStatusCode.BadRequest);

        var follow = new Follow
        {
            FollowerId = followerId,
            FollowedId = userToFollowId,
            FollowerUser = followerUser,
            FollowedUser = userToFollow
        };

        await entityFollowRepository.AddAsync(follow);

        return await Task.FromResult(true);
    }

    public async Task<bool> UnfollowUserAsync(Guid followerId, Guid userToUnfollowId)
    {
        var followRelation = await entityFollowRepository.GetFollow(followerId, userToUnfollowId);

        if (followRelation is null)
            throw new BusinessException(ApiErrorType.NotFollowing, HttpStatusCode.BadRequest);

        entityFollowRepository.Delete(followRelation);

        return await Task.FromResult(true);
    }
}
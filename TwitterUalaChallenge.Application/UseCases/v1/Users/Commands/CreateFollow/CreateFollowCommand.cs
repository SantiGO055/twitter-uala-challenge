using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateFollow;

public class CreateFollowCommand(FollowRequest followRequest) : Request<bool>
{
    public Guid FollowerId { get; set; } = followRequest.FollowerId;
    public Guid FollowedId { get; set; } = followRequest.FollowedId;
    public override bool ExecuteSaveChanges() => true;
}
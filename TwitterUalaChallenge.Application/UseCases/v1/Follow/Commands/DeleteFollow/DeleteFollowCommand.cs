using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Follow.Commands.DeleteFollow;

public class DeleteFollowCommand : Request<bool>
{
    public Guid FollowerId { get; set; }
    public Guid FollowedId { get; set; }
    public override bool ExecuteSaveChanges() => true;
}
using MediatR;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateFollow;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.DeleteFollow;

public class DeleteFollowHandler(IFollowService followService): IRequestHandler<DeleteFollowCommand, bool>
{
    public async Task<bool> Handle(DeleteFollowCommand request, CancellationToken cancellationToken)
    {
        var response = await followService.UnfollowUserAsync(request.FollowerId, request.FollowedId);

        return response;
    }
}
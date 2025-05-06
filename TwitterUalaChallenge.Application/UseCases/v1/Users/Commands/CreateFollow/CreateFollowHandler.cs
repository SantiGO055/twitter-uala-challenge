using MediatR;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateFollow;

public class CreateFollowHandler(IFollowService followService): IRequestHandler<CreateFollowCommand, bool>
{
    public async Task<bool> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
    {
        var response = await followService.FollowUserAsync(request.FollowerId, request.FollowedId);

        return response;
    }
}
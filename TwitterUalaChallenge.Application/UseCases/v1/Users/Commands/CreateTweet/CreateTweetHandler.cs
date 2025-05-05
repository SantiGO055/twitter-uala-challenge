using MediatR;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateTweet;

public class CreateTweetHandler(ITweetService contentService) : IRequestHandler<CreateTweetCommand, TweetResponse>
{
    public async Task<TweetResponse> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
    {
        var response = await contentService.CreateContentAsync(request.UserId, request.Content);

        return new TweetResponse
        {
            TweetId = response.TweetId,
            Content = response.Content,
            CreatedDate = response.CreatedDate
        };
    }
}
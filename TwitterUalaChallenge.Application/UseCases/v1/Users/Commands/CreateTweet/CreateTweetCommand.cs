using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateTweet;

public class CreateTweetCommand(Guid userId, string content) : Request<TweetResponse>
{
    public Guid UserId { get; set; } = userId;
    public string Content { get; set; } = content;
    public override bool ExecuteSaveChanges() => true;
}
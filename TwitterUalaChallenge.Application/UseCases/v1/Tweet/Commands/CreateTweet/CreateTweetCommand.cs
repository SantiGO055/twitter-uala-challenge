using System.Text.Json.Serialization;
using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Tweet.Commands.CreateTweet;

public class CreateTweetCommand(Guid userId, string content) : Request<TweetResponse>
{
    public Guid UserId { get; private set; }
    public string Content { get; set; } = content;
    public override bool ExecuteSaveChanges() => true;
}
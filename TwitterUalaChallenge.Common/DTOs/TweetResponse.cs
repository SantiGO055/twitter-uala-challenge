namespace TwitterUalaChallenge.Common.DTOs;

public class TweetResponse
{
    public Guid TweetId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}
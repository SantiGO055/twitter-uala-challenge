namespace TwitterUalaChallenge.Common.DTOs;

public class TimelineResponse
{
    public Guid TweetId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserResponse Author { get; set; }
}
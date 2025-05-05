namespace TwitterUalaChallenge.Domain.Entities;

public class Tweet
{
    public const string TableName = "ttweet";
    
    public Guid TweetId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}
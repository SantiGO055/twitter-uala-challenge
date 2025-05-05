namespace TwitterUalaChallenge.Domain.Entities;

public class Follow
{
    public const string TableName = "tfollow";
    
    public Guid FollowerId { get; set; }
    public Guid FollowedId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    
    public User FollowerUser { get; set; }
    public User FollowedUser { get; set; }
}
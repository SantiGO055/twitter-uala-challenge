namespace TwitterUalaChallenge.Domain.Entities;

public class User
{
    public const string TableName = "tuser";
    
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    
    public ICollection<Tweet> Tweets { get; set; }
    public ICollection<Follow> FollowersRelations { get; set; }
    public ICollection<Follow> FollowingRelations { get; set; }
}
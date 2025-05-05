using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Infrastructure.Persistence.Configuration;

public class FollowConfiguration: IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.ToTable(Follow.TableName);

        builder.HasKey(e => new { e.FollowerId, e.FollowedId })
            .HasName("pk_tfollow");

        builder.HasIndex(x => new { x.FollowerId, x.FollowedId })
            .HasDatabaseName("ix_tfollow_id");

        builder
            .Property(p => p.CreatedDate)
            .HasDefaultValueSql("now()")
            .IsRequired();

        builder
            .Property(p => p.ModifiedDate)
            .IsRequired(false);
                
        builder.HasOne(e => e.FollowerUser)
            .WithMany(u => u.FollowingRelations)
            .HasForeignKey(e => e.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);
                
        builder.HasOne(e => e.FollowedUser)
            .WithMany(u => u.FollowersRelations)
            .HasForeignKey(e => e.FollowedId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasCheckConstraint("CK_TFollow_NoSelfFollow", "follower_id <> followed_id");
        
    }
}
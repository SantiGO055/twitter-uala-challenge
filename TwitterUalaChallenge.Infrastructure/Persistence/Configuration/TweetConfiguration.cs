using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Infrastructure.Persistence.Configuration;

public class TweetConfiguration: IEntityTypeConfiguration<Tweet>
{
    public void Configure(EntityTypeBuilder<Tweet> builder)
    {
        builder.ToTable(Tweet.TableName);

        builder.Property(e => e.TweetId)
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.HasKey(e => e.TweetId)
            .HasName("pk_ttweet");

        builder.HasIndex(x => x.TweetId)
            .HasDatabaseName("ix_ttweet_id");
        
        builder.Property(e => e.UserId)
            .IsRequired();

        builder
            .Property(p => p.Content)
            .HasMaxLength(280)
            .IsRequired();

        builder
            .Property(p => p.CreatedDate)
            .HasDefaultValueSql("now()")
            .IsRequired();

        builder
            .Property(p => p.ModifiedDate)
            .IsRequired(false);

        builder
            .Property(p => p.IsDeleted)
            .HasDefaultValue(false);

        builder
            .Property(p => p.DeletedDate)
            .IsRequired(false);
        
        builder.HasOne(e => e.User)
            .WithMany(u => u.Tweets)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
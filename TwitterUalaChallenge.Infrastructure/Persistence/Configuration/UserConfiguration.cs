using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Infrastructure.Persistence.Configuration;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(User.TableName);
        
        builder.Property(x => x.UserId)
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.HasKey(e => e.UserId)
            .HasName("pk_tuser");
        
        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("ix_tuser_id");

        builder
            .Property(p => p.UserName)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(p => p.CreatedDate)
            .HasDefaultValueSql("now()")
            .IsRequired();

        builder
            .Property(p => p.ModifiedDate)
            .IsRequired(false);
    }
}
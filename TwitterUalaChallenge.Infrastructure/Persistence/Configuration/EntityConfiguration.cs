using TwitterUalaChallenge.Common.Constants;
using TwitterUalaChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TwitterUalaChallenge.Infrastructure.Persistence.Configuration
{
    public class EntityConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable(Test.TableName);

            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.Description)
                .HasMaxLength(CommonConstants.TEST_DESCRIPTION_MAXIMUM_LENGTH)
                .IsRequired();

            builder.HasIndex(p => p.Description)
                .IsUnique()
                .HasDatabaseName("IX_Test_Description_Unique");
        }
    }
}
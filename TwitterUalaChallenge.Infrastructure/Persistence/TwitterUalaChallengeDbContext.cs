using TwitterUalaChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TwitterUalaChallenge.Infrastructure.Persistence
{
    public class TwitterUalaChallengeDbContext : BaseDbContext
    {
        public TwitterUalaChallengeDbContext()
            : base(new DbContextOptions<TwitterUalaChallengeDbContext>())
        {
        }

        public TwitterUalaChallengeDbContext(DbContextOptions<TwitterUalaChallengeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Test> Entities { get; set; }

        protected override string DefaultSchemaName => "TwitterUalaChallenge";
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterUalaChallenge.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE extension IF NOT EXISTS \"uuid-ossp\"");
            
            migrationBuilder.EnsureSchema(
                name: "TwitterUalaChallenge");

            migrationBuilder.CreateTable(
                name: "tuser",
                schema: "TwitterUalaChallenge",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    user_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tuser", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tfollow",
                schema: "TwitterUalaChallenge",
                columns: table => new
                {
                    follower_id = table.Column<Guid>(type: "uuid", nullable: false),
                    followed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tfollow", x => new { x.follower_id, x.followed_id });
                    table.CheckConstraint("CK_TFollow_NoSelfFollow", "follower_id <> followed_id");
                    table.ForeignKey(
                        name: "fk_tfollow_users_followed_id",
                        column: x => x.followed_id,
                        principalSchema: "TwitterUalaChallenge",
                        principalTable: "tuser",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tfollow_users_follower_id",
                        column: x => x.follower_id,
                        principalSchema: "TwitterUalaChallenge",
                        principalTable: "tuser",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ttweet",
                schema: "TwitterUalaChallenge",
                columns: table => new
                {
                    tweet_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    content = table.Column<string>(type: "character varying(280)", maxLength: 280, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ttweet", x => x.tweet_id);
                    table.ForeignKey(
                        name: "fk_ttweet_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "TwitterUalaChallenge",
                        principalTable: "tuser",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tfollow_followed_id",
                schema: "TwitterUalaChallenge",
                table: "tfollow",
                column: "followed_id");

            migrationBuilder.CreateIndex(
                name: "ix_tfollow_id",
                schema: "TwitterUalaChallenge",
                table: "tfollow",
                columns: new[] { "follower_id", "followed_id" });

            migrationBuilder.CreateIndex(
                name: "ix_ttweet_id",
                schema: "TwitterUalaChallenge",
                table: "ttweet",
                column: "tweet_id");

            migrationBuilder.CreateIndex(
                name: "ix_ttweet_user_id",
                schema: "TwitterUalaChallenge",
                table: "ttweet",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tuser_id",
                schema: "TwitterUalaChallenge",
                table: "tuser",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tfollow",
                schema: "TwitterUalaChallenge");

            migrationBuilder.DropTable(
                name: "ttweet",
                schema: "TwitterUalaChallenge");

            migrationBuilder.DropTable(
                name: "tuser",
                schema: "TwitterUalaChallenge");
        }
    }
}

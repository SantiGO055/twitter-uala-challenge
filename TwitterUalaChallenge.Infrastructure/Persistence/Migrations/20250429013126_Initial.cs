using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TwitterUalaChallenge.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE SCHEMA IF NOT EXISTS TwitterUalaChallenge;");

            migrationBuilder.Sql(
                "CREATE extension IF NOT EXISTS \"uuid-ossp\""
            );
            
            migrationBuilder.EnsureSchema(
                name: "TwitterUalaChallenge");

            migrationBuilder.CreateTable(
                name: "Test",
                schema: "TwitterUalaChallenge",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_test", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Test_Description_Unique",
                schema: "TwitterUalaChallenge",
                table: "Test",
                column: "description",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test",
                schema: "TwitterUalaChallenge");
        }
    }
}

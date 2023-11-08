using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Game.Gambling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGamblingDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    AccountBalance = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGamblingDetails", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserGamblingDetails",
                columns: new[] { "Id", "AccountBalance", "UserId" },
                values: new object[,]
                {
                    { 1L, 10000L, "2c4cf163-3f29-4eb5-a572-06e6f4da79dc" },
                    { 2L, 100L, "8389d604-8191-4a30-af54-6f17fca78b4a" },
                    { 3L, 0L, "7ed87f88-a134-4659-be6f-edabbaddb06a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGamblingDetails");
        }
    }
}

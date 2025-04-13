using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vorex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedVolatiltyLevelValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VolatilityLevel",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "low" },
                    { 1, "medium" },
                    { 2, "high" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VolatilityLevel",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "VolatilityLevel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VolatilityLevel",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

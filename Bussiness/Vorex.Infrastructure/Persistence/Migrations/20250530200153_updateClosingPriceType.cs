using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vorex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateClosingPriceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ClosingPrice",
                table: "HistoricalPrice",
                type: "decimal(38,31)",
                precision: 38,
                scale: 31,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,18)",
                oldPrecision: 18,
                oldScale: 18);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ClosingPrice",
                table: "HistoricalPrice",
                type: "decimal(18,18)",
                precision: 18,
                scale: 18,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,31)",
                oldPrecision: 38,
                oldScale: 31);
        }
    }
}

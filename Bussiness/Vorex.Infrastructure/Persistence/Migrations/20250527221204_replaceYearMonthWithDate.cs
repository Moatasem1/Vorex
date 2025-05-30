using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vorex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class replaceYearMonthWithDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_HistoricalPrice_Month_Valid",
                table: "HistoricalPrice");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HistoricalPrice_Year_Valid",
                table: "HistoricalPrice");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "HistoricalPrice");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "HistoricalPrice");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "HistoricalPrice",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "HistoricalPrice");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "HistoricalPrice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "HistoricalPrice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_HistoricalPrice_Month_Valid",
                table: "HistoricalPrice",
                sql: "[Month] >= 1 AND [Month] <= 12");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HistoricalPrice_Year_Valid",
                table: "HistoricalPrice",
                sql: "[Year] >= 1 AND [Year] <= 9999");
        }
    }
}

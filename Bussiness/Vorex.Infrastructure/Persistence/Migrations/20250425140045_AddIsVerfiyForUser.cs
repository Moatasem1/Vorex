using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vorex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsVerfiyForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CryptoAnalysisHistoryId1",
                table: "CryptoFavorite",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CryptoAnalysisHistoryId1",
                table: "CryptoComparison",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CryptoFavorite_CryptoAnalysisHistoryId1",
                table: "CryptoFavorite",
                column: "CryptoAnalysisHistoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoComparison_CryptoAnalysisHistoryId1",
                table: "CryptoComparison",
                column: "CryptoAnalysisHistoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoComparison_CryptoAnalysisHistories_CryptoAnalysisHistoryId1",
                table: "CryptoComparison",
                column: "CryptoAnalysisHistoryId1",
                principalTable: "CryptoAnalysisHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoFavorite_CryptoAnalysisHistories_CryptoAnalysisHistoryId1",
                table: "CryptoFavorite",
                column: "CryptoAnalysisHistoryId1",
                principalTable: "CryptoAnalysisHistories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptoComparison_CryptoAnalysisHistories_CryptoAnalysisHistoryId1",
                table: "CryptoComparison");

            migrationBuilder.DropForeignKey(
                name: "FK_CryptoFavorite_CryptoAnalysisHistories_CryptoAnalysisHistoryId1",
                table: "CryptoFavorite");

            migrationBuilder.DropIndex(
                name: "IX_CryptoFavorite_CryptoAnalysisHistoryId1",
                table: "CryptoFavorite");

            migrationBuilder.DropIndex(
                name: "IX_CryptoComparison_CryptoAnalysisHistoryId1",
                table: "CryptoComparison");

            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CryptoAnalysisHistoryId1",
                table: "CryptoFavorite");

            migrationBuilder.DropColumn(
                name: "CryptoAnalysisHistoryId1",
                table: "CryptoComparison");
        }
    }
}

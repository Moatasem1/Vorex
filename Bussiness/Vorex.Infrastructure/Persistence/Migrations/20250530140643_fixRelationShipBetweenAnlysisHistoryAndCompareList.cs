using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vorex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixRelationShipBetweenAnlysisHistoryAndCompareList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CryptoComparison_CryptoAnalysisHistories_CryptoAnalysisHistoryId1",
            //    table: "CryptoComparison");

            migrationBuilder.DropIndex(
                name: "IX_CryptoComparison_CryptoAnalysisHistoryId",
                table: "CryptoComparison");

            //migrationBuilder.DropIndex(
            //    name: "IX_CryptoComparison_CryptoAnalysisHistoryId1",
            //    table: "CryptoComparison");

            //migrationBuilder.DropColumn(
            //    name: "CryptoAnalysisHistoryId1",
            //    table: "CryptoComparison");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoComparison_CryptoAnalysisHistoryId",
                table: "CryptoComparison",
                column: "CryptoAnalysisHistoryId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CryptoComparison_CryptoAnalysisHistoryId",
                table: "CryptoComparison");

            migrationBuilder.AddColumn<Guid>(
                name: "CryptoAnalysisHistoryId1",
                table: "CryptoComparison",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CryptoComparison_CryptoAnalysisHistoryId",
                table: "CryptoComparison",
                column: "CryptoAnalysisHistoryId");

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
        }
    }
}

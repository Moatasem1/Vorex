using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vorex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCryptoAnlysisHistoryId1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CryptoAnalysisHistoryId1",
                table: "CryptoFavorite");

            migrationBuilder.DropColumn(
                name: "CryptoAnalysisHistoryId1",
                table: "CryptoComparison");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

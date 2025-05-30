using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vorex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCryptoFavoriteRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptoFavorite_CryptoAnalysisHistories_CryptoAnalysisHistoryId",
                table: "CryptoFavorite");

            migrationBuilder.RenameColumn(
                name: "CryptoAnalysisHistoryId",
                table: "CryptoFavorite",
                newName: "CryptoId");

            migrationBuilder.RenameIndex(
                name: "IX_CryptoFavorite_CryptoAnalysisHistoryId",
                table: "CryptoFavorite",
                newName: "IX_CryptoFavorite_CryptoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoFavorite_Cryptos_CryptoId",
                table: "CryptoFavorite",
                column: "CryptoId",
                principalTable: "Cryptos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoFavorite_Users_UserId",
                table: "CryptoFavorite",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptoFavorite_Cryptos_CryptoId",
                table: "CryptoFavorite");

            migrationBuilder.DropForeignKey(
                name: "FK_CryptoFavorite_Users_UserId",
                table: "CryptoFavorite");

            migrationBuilder.RenameColumn(
                name: "CryptoId",
                table: "CryptoFavorite",
                newName: "CryptoAnalysisHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CryptoFavorite_CryptoId",
                table: "CryptoFavorite",
                newName: "IX_CryptoFavorite_CryptoAnalysisHistoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "CryptoAnalysisHistoryId1",
                table: "CryptoFavorite",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CryptoFavorite_CryptoAnalysisHistoryId1",
                table: "CryptoFavorite",
                column: "CryptoAnalysisHistoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoFavorite_CryptoAnalysisHistories_CryptoAnalysisHistoryId",
                table: "CryptoFavorite",
                column: "CryptoAnalysisHistoryId",
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

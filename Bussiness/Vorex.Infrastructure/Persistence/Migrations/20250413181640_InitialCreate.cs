using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vorex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolatilityLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolatilityLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cryptos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VolatilityLevelID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cryptos_VolatilityLevel_VolatilityLevelID",
                        column: x => x.VolatilityLevelID,
                        principalTable: "VolatilityLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CryptoAnalysisHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CryptoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HoldingDays = table.Column<int>(type: "int", nullable: false),
                    Risk = table.Column<decimal>(type: "decimal(18,18)", precision: 18, scale: 18, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAnalysisHistories", x => x.Id);
                    table.CheckConstraint("CK_CryptoAnalysisHistory_Amount_Positive", "[Amount] > 0");
                    table.CheckConstraint("CK_CryptoAnalysisHistory_HoldingDays_Positive", "[HoldingDays] > 0");
                    table.CheckConstraint("CK_CryptoAnalysisHistory_Risk_Positive", "[Risk] > 0");
                    table.ForeignKey(
                        name: "FK_CryptoAnalysisHistories_Cryptos_CryptoId",
                        column: x => x.CryptoId,
                        principalTable: "Cryptos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CryptoAnalysisHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    ClosingPrice = table.Column<decimal>(type: "decimal(18,18)", precision: 18, scale: 18, nullable: false),
                    CryptoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalPrice", x => x.Id);
                    table.CheckConstraint("CK_HistoricalPrice_Month_Valid", "[Month] >= 1 AND [Month] <= 12");
                    table.CheckConstraint("CK_HistoricalPrice_Year_Valid", "[Year] >= 1 AND [Year] <= 9999");
                    table.ForeignKey(
                        name: "FK_HistoricalPrice_Cryptos_CryptoId",
                        column: x => x.CryptoId,
                        principalTable: "Cryptos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CryptoComparison",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CryptoAnalysisHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoComparison", x => new { x.UserId, x.CryptoAnalysisHistoryId });
                    table.ForeignKey(
                        name: "FK_CryptoComparison_CryptoAnalysisHistories_CryptoAnalysisHistoryId",
                        column: x => x.CryptoAnalysisHistoryId,
                        principalTable: "CryptoAnalysisHistories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CryptoComparison_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CryptoFavorite",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CryptoAnalysisHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoFavorite", x => new { x.UserId, x.CryptoAnalysisHistoryId });
                    table.ForeignKey(
                        name: "FK_CryptoFavorite_CryptoAnalysisHistories_CryptoAnalysisHistoryId",
                        column: x => x.CryptoAnalysisHistoryId,
                        principalTable: "CryptoAnalysisHistories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CryptoFavorite_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAnalysisHistories_CryptoId",
                table: "CryptoAnalysisHistories",
                column: "CryptoId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAnalysisHistories_UserId",
                table: "CryptoAnalysisHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoComparison_CryptoAnalysisHistoryId",
                table: "CryptoComparison",
                column: "CryptoAnalysisHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoFavorite_CryptoAnalysisHistoryId",
                table: "CryptoFavorite",
                column: "CryptoAnalysisHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cryptos_VolatilityLevelID",
                table: "Cryptos",
                column: "VolatilityLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalPrice_CryptoId",
                table: "HistoricalPrice",
                column: "CryptoId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoComparison");

            migrationBuilder.DropTable(
                name: "CryptoFavorite");

            migrationBuilder.DropTable(
                name: "HistoricalPrice");

            migrationBuilder.DropTable(
                name: "CryptoAnalysisHistories");

            migrationBuilder.DropTable(
                name: "Cryptos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VolatilityLevel");
        }
    }
}

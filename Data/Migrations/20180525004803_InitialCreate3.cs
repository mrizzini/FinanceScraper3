using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceScraper3.Data.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Portfolio_PortfolioId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_PortfolioId",
                table: "Stocks",
                newName: "IX_Stocks_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Portfolio_PortfolioId",
                table: "Stocks",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Portfolio_PortfolioId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_PortfolioId",
                table: "Stock",
                newName: "IX_Stock_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Portfolio_PortfolioId",
                table: "Stock",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

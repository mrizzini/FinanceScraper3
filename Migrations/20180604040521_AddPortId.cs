using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceScraper3.Migrations
{
    public partial class AddPortId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PortId",
                table: "Stocks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortId",
                table: "Stocks");
        }
    }
}

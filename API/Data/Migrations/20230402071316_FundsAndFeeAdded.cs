using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Mugrations
{
    public partial class FundsAndFeeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "Users",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Funds",
                table: "Users",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Funds",
                table: "Users");
        }
    }
}

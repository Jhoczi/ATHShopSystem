using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ath_server.Migrations
{
    public partial class _003_AddOwnerCredentialsFieldToShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerCredentials",
                table: "Shops",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerCredentials",
                table: "Shops");
        }
    }
}

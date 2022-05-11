using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ath_server.Migrations
{
    public partial class _002_RemoveIdFieldInProductShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductInShops");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductInShops",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ath_server.Migrations
{
    public partial class _001_ChangeTypeOfProductListInShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShopId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInShops_ShopId",
                table: "ProductInShops",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInShops_Shops_ShopId",
                table: "ProductInShops",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInShops_Shops_ShopId",
                table: "ProductInShops");

            migrationBuilder.DropIndex(
                name: "IX_ProductInShops_ShopId",
                table: "ProductInShops");

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShopId",
                table: "Products",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FINAL.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryAndNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowStockThreshold = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "InventoryItems",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ItemName", "LowStockThreshold", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Hair Accessories", new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6937), "Handmade crochet rose for hair.", "Rose Bloom Crochet", 10, 120.00m, 5 },
                    { 2, "Keychain", new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6946), "Cute daisy keychain crochet.", "Daisy Charm Crochet", 10, 85.00m, 17 },
                    { 3, "Home Decor", new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6949), "Sunflower home decoration.", "Sunflower Delight Crochet", 10, 200.00m, 28 },
                    { 4, "Bouquet", new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6951), "Tulip bouquet arrangement.", "Tulip Garden Crochet", 10, 350.00m, 7 },
                    { 5, "Sachet / Fragrance Holder", new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6953), "Lavender sachet holder.", "Lavender Bliss Crochet", 10, 150.00m, 11 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}

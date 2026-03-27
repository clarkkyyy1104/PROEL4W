using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FINAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Product = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerName", "OrderDate", "Product", "Status" },
                values: new object[,]
                {
                    { 1, "Maria Santos", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Crochet Rose Bouquet", "Completed" },
                    { 2, "Juan dela Cruz", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sunflower Arrangement", "Pending" },
                    { 3, "Ana Reyes", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lavender Wreath", "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Role", "Status" },
                values: new object[,]
                {
                    { 1, "Geraldine", "Admin", "Active" },
                    { 2, "Henz", "Sales Associate", "Active" },
                    { 3, "Tiriz", "Florist", "Inactive" },
                    { 4, "Clark", "Florist", "Active" },
                    { 5, "JP", "Farmer", "Inactive" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

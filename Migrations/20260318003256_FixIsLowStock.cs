using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL.Migrations
{
    /// <inheritdoc />
    public partial class FixIsLowStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLowStock",
                table: "InventoryItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsLowStock" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 32, 56, 330, DateTimeKind.Local).AddTicks(2168), false });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsLowStock" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 32, 56, 330, DateTimeKind.Local).AddTicks(2174), false });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "IsLowStock" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 32, 56, 330, DateTimeKind.Local).AddTicks(2175), false });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "IsLowStock" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 32, 56, 330, DateTimeKind.Local).AddTicks(2176), false });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "IsLowStock" },
                values: new object[] { new DateTime(2026, 3, 18, 8, 32, 56, 330, DateTimeKind.Local).AddTicks(2178), false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLowStock",
                table: "InventoryItems");

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6937));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6946));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6949));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6951));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 1, 2, 44, 811, DateTimeKind.Local).AddTicks(6953));
        }
    }
}

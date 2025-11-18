using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "Products",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 18, 6, 33, 51, 30, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 11, 18, 6, 33, 51, 30, DateTimeKind.Utc).AddTicks(6156) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-222222222222"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 18, 6, 33, 51, 30, DateTimeKind.Utc).AddTicks(6652), new DateTime(2025, 11, 18, 6, 33, 51, 30, DateTimeKind.Utc).AddTicks(6653) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CategoryId", "Created", "Modified", "Price" },
                values: new object[] { "", new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(1664), new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(1797), 836.12 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-333333333333"),
                columns: new[] { "CategoryId", "Created", "Modified", "Price" },
                values: new object[] { "", new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(2355), new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(2356), 0.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-444444444444"),
                columns: new[] { "CategoryId", "Created", "Modified", "Price" },
                values: new object[] { "", new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(2359), new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(2359), 0.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 25, 9, 24, 27, 484, DateTimeKind.Utc).AddTicks(1821), new DateTime(2025, 9, 25, 9, 24, 27, 484, DateTimeKind.Utc).AddTicks(1983) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-222222222222"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 25, 9, 24, 27, 484, DateTimeKind.Utc).AddTicks(2558), new DateTime(2025, 9, 25, 9, 24, 27, 484, DateTimeKind.Utc).AddTicks(2559) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(4776), new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(4919) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-333333333333"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(5285), new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(5286) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-444444444444"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(5289), new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(5290) });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Brands");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 18, 10, 22, 31, 651, DateTimeKind.Utc).AddTicks(8417), new DateTime(2025, 11, 18, 10, 22, 31, 651, DateTimeKind.Utc).AddTicks(8647) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-222222222222"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 18, 10, 22, 31, 651, DateTimeKind.Utc).AddTicks(9161), new DateTime(2025, 11, 18, 10, 22, 31, 651, DateTimeKind.Utc).AddTicks(9162) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 11, 18, 10, 22, 31, 653, DateTimeKind.Utc).AddTicks(7596), new DateTime(2025, 11, 18, 10, 22, 31, 653, DateTimeKind.Utc).AddTicks(7793) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-333333333333"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 11, 18, 10, 22, 31, 653, DateTimeKind.Utc).AddTicks(8308), new DateTime(2025, 11, 18, 10, 22, 31, 653, DateTimeKind.Utc).AddTicks(8308) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-444444444444"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 11, 18, 10, 22, 31, 653, DateTimeKind.Utc).AddTicks(8311), new DateTime(2025, 11, 18, 10, 22, 31, 653, DateTimeKind.Utc).AddTicks(8311) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CategoryId", "DateCreated", "DateModified" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 11, 18, 6, 33, 51, 30, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 11, 18, 6, 33, 51, 30, DateTimeKind.Utc).AddTicks(6156) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-222222222222"),
                columns: new[] { "CategoryId", "DateCreated", "DateModified" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-bbbbbbbbbbbb"), new DateTime(2025, 11, 18, 6, 33, 51, 30, DateTimeKind.Utc).AddTicks(6652), new DateTime(2025, 11, 18, 6, 33, 51, 30, DateTimeKind.Utc).AddTicks(6653) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(1664), new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(1797) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-333333333333"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(2355), new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(2356) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-444444444444"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(2359), new DateTime(2025, 11, 18, 6, 33, 51, 32, DateTimeKind.Utc).AddTicks(2359) });
        }
    }
}

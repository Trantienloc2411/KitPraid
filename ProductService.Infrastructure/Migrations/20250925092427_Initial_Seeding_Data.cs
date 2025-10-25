using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Seeding_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandCode", "BrandDescription", "BrandImage", "BrandName", "CategoryId", "DateCreated", "DateModified", "IsActive", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "LOGI", "High quality peripherals and accessories", "logitech.png", "Logitech", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 9, 25, 9, 24, 27, 484, DateTimeKind.Utc).AddTicks(1821), new DateTime(2025, 9, 25, 9, 24, 27, 484, DateTimeKind.Utc).AddTicks(1983), true, false },
                    { new Guid("11111111-1111-1111-1111-222222222222"), "KEYC", "Premium customizable mechanical keyboards", "keychron.png", "Keychron", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-bbbbbbbbbbbb"), new DateTime(2025, 9, 25, 9, 24, 27, 484, DateTimeKind.Utc).AddTicks(2558), new DateTime(2025, 9, 25, 9, 24, 27, 484, DateTimeKind.Utc).AddTicks(2559), true, false }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AttributesJson", "BrandId", "Created", "IsActive", "IsDeleted", "Modified", "ProductDescription", "ProductName", "Sku", "Stock", "UserId" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), "{\"Color\":\"Gray\",\"Switch\":\"Tactile Quiet\",\"Layout\":\"US\"}", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(4776), true, false, new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(4919), "Low-profile mechanical keyboard with quiet tactile switches.", "Logitech MX Mechanical Keyboard", "LOGI-MX-KEY", 100, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("22222222-2222-2222-2222-333333333333"), "{\"Color\":\"Black\",\"Switch\":\"Gateron Brown\",\"Connectivity\":\"Bluetooth 5.1\"}", new Guid("11111111-1111-1111-1111-222222222222"), new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(5285), true, false, new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(5286), "Hot-swappable wireless mechanical keyboard with RGB backlight.", "Keychron K8 Pro", "KEYC-K8PRO", 50, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-cccccccccccc") },
                    { new Guid("22222222-2222-2222-2222-444444444444"), "{\"Color\":\"White\",\"DPI\":\"25600\",\"Buttons\":\"13\"}", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(5289), true, false, new DateTime(2025, 9, 25, 9, 24, 27, 485, DateTimeKind.Utc).AddTicks(5290), "Wireless gaming mouse with LIGHTSPEED and LIGHTSYNC RGB.", "Logitech G502 X Plus Mouse", "LOGI-G502X", 200, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-dddddddddddd") }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "ImageName", "ImagePath", "IsActive", "IsDeleted", "ProductId" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "mx_mechanical.png", "/images/products/mx_mechanical.png", true, false, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "k8_pro.png", "/images/products/k8_pro.png", true, false, new Guid("22222222-2222-2222-2222-333333333333") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "g502x.png", "/images/products/g502x.png", true, false, new Guid("22222222-2222-2222-2222-444444444444") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-333333333333"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-444444444444"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-222222222222"));
        }
    }
}

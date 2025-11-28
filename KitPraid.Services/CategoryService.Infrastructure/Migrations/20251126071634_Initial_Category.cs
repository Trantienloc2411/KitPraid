using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CategoryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "Created", "Description", "IsActive", "Modified", "ParentCategoryId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Chipset", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(6821), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Video Graphic Array (VGA)", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7391), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Motherboard", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7432), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Storage", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7505), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Power Supply Unit (PSU)", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7569), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Cooling Fan, Case", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7633), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "KitPraid's PC", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7694), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Intel", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7264), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "AMD", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7286), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "NVIDIA", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7394), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "AMD", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7405), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "Brands", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7418), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000032"), "Intel Platform", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7474), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000036"), "AMD Platform", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7483), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), "Form Factor", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7494), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000044"), "SSD", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7508), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), "HDD", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7548), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000052"), "Brands", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7557), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000057"), "Wattage", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7572), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000061"), "Efficiency", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7582), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000065"), "Brands", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7621), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000070"), "CPU Cooling", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7635), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000074"), "Case Fans", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7644), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000078"), "PC Cases", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7655), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000082"), "Gaming PC", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7696), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000086"), "Workstation PC", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7705), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000090"), "Office PC", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7715), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Intel 9", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7271), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Intel 7", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7275), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Intel 5", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7278), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Intel 3", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7284), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "AMD R3", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7289), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "AMD R5", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7378), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "AMD R7", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7382), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "AMD R9", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7387), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "RTX 4000 Series", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7396), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "RTX 3000 Series", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7400), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "GTX 1000 Series", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7403), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "Radeon RX 7000", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7408), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "Radeon RX 6000", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7411), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "Radeon RX 5000", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7414), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "ASUS", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7421), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000027") },
                    { new Guid("00000000-0000-0000-0000-000000000029"), "MSI", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7423), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000027") },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "Gigabyte", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7427), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000027") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "EVGA", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7430), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000027") },
                    { new Guid("00000000-0000-0000-0000-000000000033"), "Z790", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7476), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000032") },
                    { new Guid("00000000-0000-0000-0000-000000000034"), "B760", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7479), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000032") },
                    { new Guid("00000000-0000-0000-0000-000000000035"), "H710", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7481), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000032") },
                    { new Guid("00000000-0000-0000-0000-000000000037"), "X670", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7486), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000036") },
                    { new Guid("00000000-0000-0000-0000-000000000038"), "B650", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7489), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000036") },
                    { new Guid("00000000-0000-0000-0000-000000000039"), "A520", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7492), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000036") },
                    { new Guid("00000000-0000-0000-0000-000000000041"), "ATX", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7497), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000040") },
                    { new Guid("00000000-0000-0000-0000-000000000042"), "Micro-ATX", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7500), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000040") },
                    { new Guid("00000000-0000-0000-0000-000000000043"), "Mini-ITX", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7502), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000040") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), "NVMe M.2", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7510), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000044") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), "SATA 2.5\"", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7513), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000044") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), "PCIe 4.0", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7515), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000044") },
                    { new Guid("00000000-0000-0000-0000-000000000049"), "3.5\" Desktop", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7551), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000048") },
                    { new Guid("00000000-0000-0000-0000-000000000050"), "2.5\" Laptop", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7553), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000048") },
                    { new Guid("00000000-0000-0000-0000-000000000051"), "Enterprise", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7555), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000048") },
                    { new Guid("00000000-0000-0000-0000-000000000053"), "Samsung", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7560), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000052") },
                    { new Guid("00000000-0000-0000-0000-000000000054"), "Western Digital", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7562), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000052") },
                    { new Guid("00000000-0000-0000-0000-000000000055"), "Seagate", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7565), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000052") },
                    { new Guid("00000000-0000-0000-0000-000000000056"), "Crucial", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7567), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000052") },
                    { new Guid("00000000-0000-0000-0000-000000000058"), "500W-650W", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7574), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000057") },
                    { new Guid("00000000-0000-0000-0000-000000000059"), "700W-850W", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7576), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000057") },
                    { new Guid("00000000-0000-0000-0000-000000000060"), "1000W+", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7579), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000057") },
                    { new Guid("00000000-0000-0000-0000-000000000062"), "80+ Gold", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7584), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000061") },
                    { new Guid("00000000-0000-0000-0000-000000000063"), "80+ Platinum", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7586), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000061") },
                    { new Guid("00000000-0000-0000-0000-000000000064"), "80+ Titanium", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7589), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000061") },
                    { new Guid("00000000-0000-0000-0000-000000000066"), "Corsair", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7623), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000065") },
                    { new Guid("00000000-0000-0000-0000-000000000067"), "EVGA", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7625), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000065") },
                    { new Guid("00000000-0000-0000-0000-000000000068"), "Seasonic", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7628), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000065") },
                    { new Guid("00000000-0000-0000-0000-000000000069"), "be quiet!", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7631), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000065") },
                    { new Guid("00000000-0000-0000-0000-000000000071"), "Air Coolers", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7637), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000070") },
                    { new Guid("00000000-0000-0000-0000-000000000072"), "AIO Liquid Coolers", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7640), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000070") },
                    { new Guid("00000000-0000-0000-0000-000000000073"), "Custom Loops", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7642), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000070") },
                    { new Guid("00000000-0000-0000-0000-000000000075"), "120mm", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7647), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000074") },
                    { new Guid("00000000-0000-0000-0000-000000000076"), "140mm", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7650), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000074") },
                    { new Guid("00000000-0000-0000-0000-000000000077"), "RGB Fans", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7652), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000074") },
                    { new Guid("00000000-0000-0000-0000-000000000079"), "Full Tower", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7687), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000078") },
                    { new Guid("00000000-0000-0000-0000-000000000080"), "Mid Tower", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7690), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000078") },
                    { new Guid("00000000-0000-0000-0000-000000000081"), "Mini ITX", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7692), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000078") },
                    { new Guid("00000000-0000-0000-0000-000000000083"), "Entry Level", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7698), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000082") },
                    { new Guid("00000000-0000-0000-0000-000000000084"), "Mid Range", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7701), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000082") },
                    { new Guid("00000000-0000-0000-0000-000000000085"), "High End", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7703), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000082") },
                    { new Guid("00000000-0000-0000-0000-000000000087"), "Content Creation", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7708), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000086") },
                    { new Guid("00000000-0000-0000-0000-000000000088"), "CAD/3D Modeling", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7710), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000086") },
                    { new Guid("00000000-0000-0000-0000-000000000089"), "Video Editing", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7713), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000086") },
                    { new Guid("00000000-0000-0000-0000-000000000091"), "Budget", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7717), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000090") },
                    { new Guid("00000000-0000-0000-0000-000000000092"), "Standard", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7720), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000090") },
                    { new Guid("00000000-0000-0000-0000-000000000093"), "Premium", new DateTime(2025, 11, 26, 7, 16, 33, 723, DateTimeKind.Utc).AddTicks(7722), null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000090") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

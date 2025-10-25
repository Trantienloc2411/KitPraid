using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        var logiId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var keycId = Guid.Parse("11111111-1111-1111-1111-222222222222");

        var product1Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var product2Id = Guid.Parse("22222222-2222-2222-2222-333333333333");
        var product3Id = Guid.Parse("22222222-2222-2222-2222-444444444444");

        builder.HasData(
            new Product
            {
                Id = product1Id,
                ProductName = "Logitech MX Mechanical Keyboard",
                ProductDescription = "Low-profile mechanical keyboard with quiet tactile switches.",
                BrandId = logiId,
                Sku = "LOGI-MX-KEY",
                Stock = 100,
                AttributesJson = "{\"Color\":\"Gray\",\"Switch\":\"Tactile Quiet\",\"Layout\":\"US\"}",
                IsActive = true,
                IsDeleted = false,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                Price = 836.12,
                UserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
            },
            new Product
            {
                Id = product2Id,
                ProductName = "Keychron K8 Pro",
                ProductDescription = "Hot-swappable wireless mechanical keyboard with RGB backlight.",
                BrandId = keycId,
                Sku = "KEYC-K8PRO",
                Stock = 50,
                AttributesJson = "{\"Color\":\"Black\",\"Switch\":\"Gateron Brown\",\"Connectivity\":\"Bluetooth 5.1\"}",
                IsActive = true,
                IsDeleted = false,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                UserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-cccccccccccc")
            },
            new Product
            {
                Id = product3Id,
                ProductName = "Logitech G502 X Plus Mouse",
                ProductDescription = "Wireless gaming mouse with LIGHTSPEED and LIGHTSYNC RGB.",
                BrandId = logiId,
                Sku = "LOGI-G502X",
                Stock = 200,
                AttributesJson = "{\"Color\":\"White\",\"DPI\":\"25600\",\"Buttons\":\"13\"}",
                IsActive = true,
                IsDeleted = false,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                UserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-dddddddddddd")
            }
        );
    }
}

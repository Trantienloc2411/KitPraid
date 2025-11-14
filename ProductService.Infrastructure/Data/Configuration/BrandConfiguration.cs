using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Configuration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        var logiId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var keycId = Guid.Parse("11111111-1111-1111-1111-222222222222");

        builder.HasData(
            new Brand
            {
                Id = logiId,
                BrandCode = "LOGI",
                BrandName = "Logitech",
                BrandDescription = "High quality peripherals and accessories",
                BrandImage = "logitech.png",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            },
            new Brand
            {
                Id = keycId,
                BrandCode = "KEYC",
                BrandName = "Keychron",
                BrandDescription = "Premium customizable mechanical keyboards",
                BrandImage = "keychron.png",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            }
        );
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Configuration;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasData(
            new Image
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                ImageName = "mx_mechanical.png",
                ImagePath = "/images/products/mx_mechanical.png",
                ProductId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                IsActive = true,
                IsDeleted = false
            },
            new Image
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                ImageName = "k8_pro.png",
                ImagePath = "/images/products/k8_pro.png",
                ProductId = Guid.Parse("22222222-2222-2222-2222-333333333333"),
                IsActive = true,
                IsDeleted = false
            },
            new Image
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                ImageName = "g502x.png",
                ImagePath = "/images/products/g502x.png",
                ProductId = Guid.Parse("22222222-2222-2222-2222-444444444444"),
                IsActive = true,
                IsDeleted = false
            }
        );
    }
}
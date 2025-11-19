using ProductService.Application.Dtos;
using ProductService.Domain.Entities;

namespace ProductService.Application.Mapping;

public static class BrandMapper
{
    public static Brand ToBrand(CreateBrandDto branddtos)
    {
        var brandNew = new Brand
        {
            Id = Guid.NewGuid(),
            BrandCode = branddtos.BrandCode,
            BrandDescription = branddtos.Description,
            BrandImage = branddtos.BrandImageUrl,
            BrandName = branddtos.BrandName,
            IsDeleted = false,
            IsActive = branddtos.IsActive
        };
        return brandNew;
    }

    public static void ApplyUpdate(Brand brand, UpdateBrandDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.BrandName)) brand.BrandName = dto.BrandName;
        if(!string.IsNullOrWhiteSpace(dto.Description)) brand.BrandDescription = dto.Description;
        if(!string.IsNullOrWhiteSpace(dto.BrandImageUrl)) brand.BrandImage = dto.BrandImageUrl;
        brand.DateModified = DateTime.UtcNow;
        brand.IsActive = dto.IsActive;
    }

    public static GetBrandDto ToGetBrandDto(Brand brand)
    {
        return new GetBrandDto
        {
            Id = brand.Id,
            BrandCode = brand.BrandCode,
            BrandName = brand.BrandName,
            Description = brand.BrandDescription,
            BrandImageUrl = brand.BrandImage
        };
    }
}
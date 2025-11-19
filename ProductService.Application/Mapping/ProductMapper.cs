using System.Diagnostics;
using ProductService.Application.Dtos;
using ProductService.Domain.Entities;

namespace ProductService.Application.Mapping;

public static class ProductMapper
{
    public static Product ToCreateProduct(CreateProductDto dto)
    {
        return new Product
        {
            Id = Guid.NewGuid(), // or let DB generate
            ProductName = dto.ProductName,
            ProductDescription = dto.ProductDescription,
            Price = dto.Price,
            Sku = dto.Sku,
            BrandId = dto.BrandId,
            CategoryId = dto.CategoryId,   // IMPORTANT if category is required
            Stock = dto.Stock,
            IsActive = dto.IsActive,
            IsDeleted = false,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            UserId = dto.UserId,
            Attributes = dto.Attributes
        };
    }
    
    public static void ApplyUpdate(Product product, UpdateProductDto dto)
    {
        // Only update fields that are provided (not null)

        if (!string.IsNullOrWhiteSpace(dto.ProductName))
            product.ProductName = dto.ProductName;

        if (!string.IsNullOrWhiteSpace(dto.ProductDescription))
            product.ProductDescription = dto.ProductDescription;

        if (dto.BrandId.HasValue)
            product.BrandId = dto.BrandId.Value;

        if (dto.Price > 0)
            product.Price = dto.Price;

        if (dto.Stock >= 0)
            product.Stock = dto.Stock;

        if (!string.IsNullOrWhiteSpace(dto.CategoryId))
            product.CategoryId = dto.CategoryId;

        if (dto.IsActive.HasValue)
            product.IsActive = dto.IsActive.Value;

        // Attributes (JSON column)
        if (dto.Attributes is not null)
            product.Attributes = dto.Attributes;

        // Always update Modified timestamp
        product.Modified = DateTime.UtcNow;
    }

    public static GetProductDto ToGetProductDto(Product product)
    {
        Debug.Assert(product.Images != null, "product.Images != null");
        return new GetProductDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Sku = product.Sku,
            Price = product.Price,
            Stock = product.Stock,
            Attributes = product.Attributes,
            BrandName = product.Brand?.BrandName,
            CategoryId = product.CategoryId,
            Images = product.Images.ToList()
        };
    }
    
    public static GetProductDetailDto ToGetProductDetailDto(Product product)
    {
        return new GetProductDetailDto
        {
            ProductId = product.Id,
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            Sku = product.Sku,
            Price = product.Price,
            Stock = product.Stock,
            Attributes = product.Attributes,
            BrandDto = BrandMapper.ToGetBrandDto(product.Brand),
            CategoryId = product.CategoryId,
            Images = product.Images.ToList()
        };
    }
    
    
}
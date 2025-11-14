using ProductService.Application.Dtos;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Mapping;

public class ProductMapper
{
    public static Product ToProduct(CreateProductDtos product)
    {
       
        return new Product
        {
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            Price = product.Price,
            Sku = product.Sku,
            BrandId = product.BrandId,



        };
    }
}
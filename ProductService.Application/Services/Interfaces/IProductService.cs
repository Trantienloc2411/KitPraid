using System;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Application.Services;

public interface IProductService
{
    public Task<PageResult<Product>> GetAllProductsAsync(PageRequest pageRequest);
    public Task<Product?> GetProductByIdAsync(string id);
    public Task<Product> UpdateProductAsync(Product product);
    public Task<Product> CreateProductAsync(Product product);
    public Task<bool> DeleteProductAsync(string id);

}

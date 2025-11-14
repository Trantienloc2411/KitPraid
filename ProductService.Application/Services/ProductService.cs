using System;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;

namespace ProductService.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public Task<Product> CreateProductAsync(Product product)
    {
        return productRepository.AddProductAsync(product);
    }

    public Task<bool> DeleteProductAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<PageResult<Product>> GetAllProductsAsync(PageRequest pageRequest)
    {
        return productRepository.GetAllAsync(pageRequest);
    }

    public Task<Product?> GetProductByIdAsync(string id)
    {
        return productRepository.GetByIdAsync(id);
    }

    public Task<Product> UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}

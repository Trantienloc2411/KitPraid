using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Domain.Repositories;

public interface IProductRepository
{
    Task<PageResult<Product>> GetAllAsync(PageRequest pageRequest);
    Task<Product?> GetByIdAsync(string id);
    Task<Product> SaveAsync(Product product);
    Task<bool> DeleteAsync(string id);
    Task<PageResult<Product>> GetAllProductsWithKeywordAsync(PageRequest pageRequest, string keyword);
    Task<Product?> AddProductAsync(Product product);
    
}
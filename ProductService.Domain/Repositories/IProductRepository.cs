using ProductService.Domain.Entities;

namespace ProductService.Domain.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(string id);
    Task<Product> SaveAsync(Product product);
    Task<bool> DeleteAsync(string id);
    
}
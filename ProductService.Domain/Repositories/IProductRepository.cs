using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Domain.Repositories;

public interface IProductRepository
{
    Task<OperationResult<PageResult<Product>>> GetAllAsync(PageRequest pageRequest);
    Task<OperationResult<Product>> GetByIdAsync(string id);
    Task<OperationResult<Product>> SaveAsync(Product product);
    Task<OperationResult<bool>> DeleteAsync(string id);
    Task<OperationResult<PageResult<Product>>> GetAllProductsWithKeywordAsync(PageRequest pageRequest, string keyword);
    Task<OperationResult<Product>> AddProductAsync(Product product);
    Task<OperationResult<Product>> UpdateAsync(Product product);
    
    
    
}
using CategoryService.Domain.Entities;
using CategoryService.Domain.ValueObjects;

namespace CategoryService.Domain.Repositories;

public interface ICategoryRepository
{
    public Task<OperationResult<IEnumerable<Category>>> GetAllAsync(PageRequest?  pageRequest = null);
    public Task<OperationResult<List<Category?>>> GetAllAsync();
    public Task<OperationResult<Category?>> GetByIdAsync(Guid id);
    public Task<OperationResult<Category>> CreateAsync(Category category);
    public Task<OperationResult<Category?>> UpdateAsync(Guid id,Category category);
    public Task<OperationResult<bool>> DeleteAsync(Guid id);
}
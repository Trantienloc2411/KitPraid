using CategoryService.Domain.Models;

namespace CategoryService.Domain.Repositories;

public interface ICategoryRepository
{
    public Task<OperationResult<List<Category>>> GetAllCategoriesAsync();
    public Task<OperationResult<Category>> GetCategoryByIdAsync(Guid id);
    public Task<OperationResult<Category>> CreateCategoryAsync(Category category);
    public Task<OperationResult<bool>> DeleteCategoryAsync(Guid id);
    public Task<OperationResult<Category>> UpdateCategoryAsync(Guid id, Category category);
}
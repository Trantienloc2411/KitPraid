using CategoryService.Application.Dtos;
using CategoryService.Domain;
using CategoryService.Domain.Entities;
using CategoryService.Domain.ValueObjects;

namespace CategoryService.Application.Services.Interfaces;

public interface ICategoryService
{
    public Task<OperationResult<ICollection<Category>>> GetAllAsync(PageRequest pageRequest = null);
    Task<OperationResult<List<CategoryTreeDto>>> GetAllTreeAsync();

    public Task<OperationResult<CategoryDto?>> GetByIdAsync(Guid id);
    public Task<OperationResult<Category>> CreateAsync(CreateCategoryDto category);
    public Task<OperationResult<Category?>> UpdateAsync(Guid id, UpdateCategoryDto category);
    public Task<OperationResult<bool>> DeleteAsync(Guid id);
    
}
using CategoryService.Domain;
using CategoryService.Domain.Entities;
using CategoryService.Domain.Repositories;
using CategoryService.Domain.ValueObjects;
using CategoryService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Infrastructure.Repositories;

public class CategoryRepository(CategoryDbContext context) : ICategoryRepository
{
    public async Task<OperationResult<IEnumerable<Category>>> GetAllAsync(PageRequest? pageRequest)
    {
        var query = context.Categories.AsQueryable();

        var result = await query
            .OrderBy(c => c.CategoryName)
            .Skip(pageRequest.Skip)
            .Take(pageRequest.Size)
            .ToListAsync();

        return OperationResult<IEnumerable<Category>>.Ok(result);
    }

    public async Task<OperationResult<List<Category?>>> GetAllAsync()
    {
        var result = await context.Categories.AsNoTracking().
            OrderBy(c => c.CategoryName).ToListAsync();
        
        return OperationResult<List<Category?>>.Ok(result);
    }

    public async Task<OperationResult<Category?>> GetByIdAsync(Guid id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        return OperationResult<Category?>.Ok(category);
    }

    public async Task<OperationResult<Category>> CreateAsync(Category category)
    {
        var newCategory = await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
        return OperationResult<Category>.Ok(newCategory.Entity);
    }

    public async Task<OperationResult<Category?>> UpdateAsync(Guid id, Category category)
    {
        var result = context.Categories.Update(category);
        await context.SaveChangesAsync();
        return OperationResult<Category?>.Ok(result.Entity);
    }

    public async Task<OperationResult<bool>> DeleteAsync(Guid id)
    {
        var result = await context.Categories.FindAsync(id);
        if (result is null)
        {
            return OperationResult<bool>.Fail($"Category with ID {id} not found.");
        }
        context.Categories.Remove(result);
        await context.SaveChangesAsync();
        return OperationResult<bool>.Ok(true);
    }
}
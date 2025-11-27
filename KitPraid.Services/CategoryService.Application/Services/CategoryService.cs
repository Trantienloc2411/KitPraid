using System.Diagnostics;
using CategoryService.Application.Dtos;
using CategoryService.Application.Services.Interfaces;
using CategoryService.Domain;
using CategoryService.Domain.Entities;
using CategoryService.Domain.Repositories;
using CategoryService.Domain.ValueObjects;

namespace CategoryService.Application.Services;

public class CategoryService (ICategoryRepository repo)  : ICategoryService 
{
    public async Task<OperationResult<ICollection<Category>>> GetAllAsync(PageRequest pageRequest)
    {
        try
        {
            var result = await repo.GetAllAsync(pageRequest);
            if(!result.Success)
            {
                Debug.Assert(result.Error != null, "result.Error != null");
                return OperationResult<ICollection<Category>>.Fail(result.Error);
            }

            Debug.Assert(result.Data != null, "result.Data != null");
            return OperationResult<ICollection<Category>>.Ok(result.Data.ToList());
        }
        catch (Exception e)
        {
            return OperationResult<ICollection<Category>>.Fail(e.Message);
        }
    }

    public async Task<OperationResult<List<CategoryTreeDto>>> GetAllTreeAsync()
    {
        try
        {
            var result = await repo.GetAllAsync();
            if (!result.Success)
            {
                return OperationResult<List<CategoryTreeDto>>.Fail(result.Error);
            }

            var categories = result.Data!;
            var tree = BuildTree(categories);

            return OperationResult<List<CategoryTreeDto>>.Ok(tree);
        }
        catch (Exception e)
        {
            return OperationResult<List<CategoryTreeDto>>.Fail(e.Message);
        }
    }

    public async Task<OperationResult<CategoryDto?>> GetByIdAsync(Guid id)
    {
        try
        {
            var result = await repo.GetByIdAsync(id);
            if (!result.Success) return OperationResult<CategoryDto?>.Fail(result.Error);

            var c = result.Data;

            if (c is null)
                return OperationResult<CategoryDto?>.Fail("Category not found");

            var dto = new CategoryDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Description = c.Description,
                ParentCategoryId = c.ParentCategoryId,
                IsActive = c.IsActive
            };
            return OperationResult<CategoryDto?>.Ok(dto);
        }
        catch (Exception e)
        {
            return OperationResult<CategoryDto?>.Fail(e.Message);
        }
    }

    private List<CategoryTreeDto> BuildTree(List<Category> categories)
    {
        var lookup = categories.ToLookup(c => c.ParentCategoryId);

        return Build(null);

        List<CategoryTreeDto> Build(Guid? parentId)
        {
            return lookup[parentId]
                .Select(c => new CategoryTreeDto
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    IsActive = c.IsActive,
                    ParentCategoryId = c.ParentCategoryId,
                    ChildCategories = Build(c.Id)
                })
                .ToList();
        }
    }

    public async Task<OperationResult<Category>> CreateAsync(CreateCategoryDto categoryDto)
    {
        try
        {
            var newProduct = new Category
            {
                CategoryName = categoryDto.CategoryName,
                IsActive = categoryDto.IsActive,
                Description = categoryDto.Description,
                ParentCategoryId = categoryDto.ParentCategoryId
            };
            var result = await repo.CreateAsync(newProduct);
            return !result.Success ? OperationResult<Category>.Fail(result.Error) : OperationResult<Category>.Ok(result.Data);
        }
        catch (Exception e)
        {
            return OperationResult<Category>.Fail(e.Message);
        }
    }

    public async Task<OperationResult<Category?>> UpdateAsync(Guid id, UpdateCategoryDto category)
    {
        try
        {
            var result = await repo.UpdateAsync(id, new Category
            {
                CategoryName = category.CategoryName,
                IsActive = category.IsActive,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId,
                Modified = DateTime.UtcNow
            });
            return !result.Success ? OperationResult<Category?>.Fail(result.Error) : OperationResult<Category?>.Ok(result.Data);
        }
        catch (Exception e)
        {
            return OperationResult<Category?>.Fail(e.Message);
        }
    }

    public async Task<OperationResult<bool>> DeleteAsync(Guid id)
    {
        try
        {
            var result = await repo.DeleteAsync(id);
            return result.Success ? OperationResult<bool>.Ok(true) : OperationResult<bool>.Fail(result.Error);
        }
        catch (Exception e)
        {
            return OperationResult<bool>.Fail(e.Message);
        }
    }
}
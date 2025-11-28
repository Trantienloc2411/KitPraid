using Microsoft.EntityFrameworkCore;
using ProductService.Application.Dtos;
using ProductService.Application.Mapping;
using ProductService.Application.Services.Interfaces;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;

namespace ProductService.Application.Services;

public class BrandService(IBrandRepository brandRepository)  : IBrandService
{
    public async Task<OperationResult<PageResult<GetBrandDto>>> GetAllBrandsAsync(PageRequest pageRequest)
    {
        try
        {
            var result = await brandRepository.GetAllBrandsAsync(pageRequest);
            if (!result.Success)
            {
                return OperationResult<PageResult<GetBrandDto>>.Fail(result.Error!);
            }
            
            var mappedItems = result.Data?.Items.Select(BrandMapper.ToGetBrandDto).ToList();
            if (mappedItems != null)
            {
                if (result.Data != null)
                {
                    var pageResult = new PageResult<GetBrandDto>(
                        mappedItems, 
                        result.Data.TotalCount, 
                        result.Data.Page, 
                        result.Data.Size
                    );
            
                    return OperationResult<PageResult<GetBrandDto>>.Ok(pageResult);
                }
            }
            return OperationResult<PageResult<GetBrandDto>>.Fail("No brands found");
        }
        catch (Exception e)
        {
            return OperationResult<PageResult<GetBrandDto>>.Fail($"Some unexpected error. Details : {e.Message}");
        }
    }

    public async Task<OperationResult<int>> CountAllBrandsAsync()
    {
        try
        {
            return await brandRepository.CountAllBrandsAsync();
        }
        catch (Exception e)
        {
            return OperationResult<int>.Fail($"Some unexpected error. Details : {e.Message}");
        }
    }

    public async Task<OperationResult<PageResult<GetBrandItemsDto>>> GetAllItemBrandsAsync(PageRequest pageRequest, Guid brandId)
    {
        try
        {   
            var result =  await brandRepository.GetAllItemBrandsAsync(pageRequest, brandId);
            if (!result.Success)
            {
                return OperationResult<PageResult<GetBrandItemsDto>>.Fail(result.Error!);
            }
            
            var mappedItems = result.Data?.Items.Select(BrandMapper.ToGetBrandItemDto).ToList();
            if (mappedItems != null)
            {
                if (result.Data != null)
                {
                    var pageResult = new PageResult<GetBrandItemsDto>(
                        mappedItems, 
                        result.Data.TotalCount, 
                        result.Data.Page, 
                        result.Data.Size
                    );
            
                    return OperationResult<PageResult<GetBrandItemsDto>>.Ok(pageResult);
                }
            }
            return OperationResult<PageResult<GetBrandItemsDto>>.Fail("No items found");
        }
        catch (Exception e)
        {
            return OperationResult<PageResult<GetBrandItemsDto>>.Fail($"Some unexpected error. Details : {e.Message}");
        }

        
    }

    public async Task<OperationResult<GetBrandDto>> GetBrandAsync(Guid brandId)
    {
        try
        {

            var result =  await brandRepository.GetBrandAsync(brandId);
            if (result.Data != null)
            {
                var mapped = BrandMapper.ToGetBrandDto(result.Data);
                return OperationResult<GetBrandDto>.Ok(mapped);
            }
            return OperationResult<GetBrandDto>.Fail("Brand not found");
        }
        catch (Exception e)
        {
            return OperationResult<GetBrandDto>.Fail($"Some unexpected error. Details : {e.Message}");    
        }
    }



    public async Task<OperationResult<Brand>> DeleteBrandAsync(Guid brandId)
    {
        try
        {
            return await brandRepository.DeleteBrandAsync(brandId);
        }
        catch (Exception e)
        {
            return OperationResult<Brand>.Fail($"Some unexpected error. Details : {e.Message}");
        }
    }

    public async Task<OperationResult<Brand>> UpdateBrandAsync(string id, UpdateBrandDto brand)
    {
        try
        {
            var brandOp = await brandRepository.GetBrandAsync(Guid.Parse(id));
            if(!brandOp.Success) return brandOp;
            else
            {
                if (brandOp.Data != null) BrandMapper.ApplyUpdate(brandOp.Data, brand);
            }

            if (brandOp.Data != null) return await brandRepository.UpdateBrandAsync(brandOp.Data);
            return brandOp;
        }
        catch (Exception e)
        {
            return OperationResult<Brand>.Fail($"Some unexpected error. Details : {e.Message}");
        }
    }

    public async Task<OperationResult<Brand>> CreateBrandAsync(CreateBrandDto brand)
    {
        try
        {
            var brandExisted = brandRepository.GetBrandByBrandCodeAsync(brand.BrandCode);
            if (brandExisted.Result.Success) return OperationResult<Brand>.Fail("Brand code already existed");
            var brandCreate = BrandMapper.ToBrand(brand);
            brandCreate.Id = Guid.NewGuid();
            brandCreate.IsDeleted = false;
            brandCreate.IsActive = true;
            brandCreate.DateCreated = DateTime.UtcNow;
            
            var result = await brandRepository.AddBrandAsync(brandCreate);
            return result;
        }
        catch (DbUpdateException e)
        {
            return OperationResult<Brand>.Fail($"There are error when saving data. \n Error : {e.Message}");
        }
    }
}
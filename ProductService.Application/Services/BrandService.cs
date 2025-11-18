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
    public async Task<OperationResult<PageResult<Brand>>> GetAllBrandsAsync(PageRequest pageRequest)
    {
        try
        {
            var result = await brandRepository.GetAllBrandsAsync(pageRequest);
            return result;
        }
        catch (Exception e)
        {
            return OperationResult<PageResult<Brand>>.Fail($"Some unexpected error. Details : {e.Message}");
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

    public async Task<OperationResult<PageResult<Brand>>> GetAllItemBrandsAsync(PageRequest pageRequest, Guid brandId)
    {
        try
        {   
            return await brandRepository.GetAllItemBrandsAsync(pageRequest, brandId);
        }
        catch (Exception e)
        {
            return OperationResult<PageResult<Brand>>.Fail($"Some unexpected error. Details : {e.Message}");
        }
    }

    public async Task<OperationResult<Brand>> GetBrandAsync(Guid brandId)
    {
        try
        {
            return await brandRepository.GetBrandAsync(brandId);
        }
        catch (Exception e)
        {
            return OperationResult<Brand>.Fail($"Some unexpected error. Details : {e.Message}");    
        }
    }

    public async Task<OperationResult<Brand>> SaveBrandAsync(Brand brand)
    {
        try
        {
            return await brandRepository.SaveBrandAsync(brand);
        }
        catch (Exception e)
        {
            return OperationResult<Brand>.Fail($"Some unexpected error. Details : {e.Message}");
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
            var brandOP = await brandRepository.GetBrandAsync(Guid.Parse(id));
            if(!brandOP.Success) return brandOP;
            else
            {
                BrandMapper.ApplyUpdate(brandOP.Data, brand);
            }
            return await brandRepository.UpdateBrandAsync(brandOP.Data);
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
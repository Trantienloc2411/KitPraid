using System;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Application.Services.Interfaces;

public interface IBrandService
{
    public Task<PageResult<Brand>> GetAllBrandsAsync(PageRequest pageRequest);
    public Task<int> CountAllBrandsAsync();
    public Task<PageResult<Brand>> GetAllItemBrandsAsync(PageRequest pageRequest, Guid brandId);
    public Task<Brand> GetBrandAsync(Guid brandId);
    public Task<Brand> SaveBrandAsync(Brand brand);
    public Task<Brand> DeleteBrandAsync(Guid brandId);
    public Task<Brand> UpdateBrandAsync(Brand brand);
}

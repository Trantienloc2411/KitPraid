using System;
using ProductService.Application.Dtos;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Application.Services.Interfaces;

public interface IBrandService
{
    public Task<OperationResult<PageResult<Brand>>> GetAllBrandsAsync(PageRequest pageRequest);
    public Task<OperationResult<int>> CountAllBrandsAsync();
    public Task<OperationResult<PageResult<Brand>>> GetAllItemBrandsAsync(PageRequest pageRequest, Guid brandId);
    public Task<OperationResult<Brand>> GetBrandAsync(Guid brandId);
    public Task<OperationResult<Brand>> SaveBrandAsync(Brand brand);
    public Task<OperationResult<Brand>> DeleteBrandAsync(Guid brandId);
    public Task<OperationResult<Brand>> UpdateBrandAsync(string id, UpdateBrandDto brand);
    public Task<OperationResult<Brand>> CreateBrandAsync(CreateBrandDto brand);
}

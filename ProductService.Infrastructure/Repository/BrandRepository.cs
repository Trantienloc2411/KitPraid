using Microsoft.EntityFrameworkCore;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repository;

public class BrandRepository : IBrandRepository
{
    private readonly ProductDbContext _dbContext;

    public BrandRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<OperationResult<PageResult<Brand>>> GetAllBrandsAsync(PageRequest pageRequest)
    {
        var totalRecords = await _dbContext.Brands.CountAsync();

        var brands = await _dbContext.Brands
            .Skip((pageRequest.Page - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .ToListAsync();
        var pageResult = new PageResult<Brand>(brands, totalRecords, pageRequest.Page, pageRequest.Size);
        return OperationResult<PageResult<Brand>>.Ok(pageResult);
    }

    public async Task<OperationResult<int>> CountAllBrandsAsync()
    {
        var result =  await _dbContext.Brands.CountAsync();
        return OperationResult<int>.Ok(result);
    }

    public async Task<OperationResult<PageResult<Brand>>> GetAllItemBrandsAsync(PageRequest pageRequest, Guid brandId)
    {
        var totalRecords = _dbContext.Brands.Count(b => b.Id == brandId);
        var brands = await _dbContext.Brands
            .Include(b => b.Products)
            .Where(b => b.Id == brandId)
            .Skip((pageRequest.Page - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .ToListAsync();
        var pageResult = new PageResult<Brand>(brands, totalRecords, pageRequest.Page, pageRequest.Size);
        return OperationResult<PageResult<Brand>>.Ok(pageResult);
    }

    public async Task<OperationResult<Brand>> GetBrandAsync(Guid brandId)
    {
        var brand = await _dbContext.Brands.FindAsync(brandId);
        return brand == null ? OperationResult<Brand>.Fail("Brand not found.") : OperationResult<Brand>.Ok(brand);
    }

    public async Task<OperationResult<Brand>> SaveBrandAsync(Brand brand)
    {
        _dbContext.Brands.Add(brand);
        await _dbContext.SaveChangesAsync();
        return OperationResult<Brand>.Ok(brand);
    }

    public async Task<OperationResult<Brand>> DeleteBrandAsync(Guid brandId)
    {
        var brand = await _dbContext.Brands.FindAsync(brandId) ?? throw new InvalidOperationException($"Brand with ID {brandId} not found.");
        brand.IsDeleted = true;
        await _dbContext.SaveChangesAsync();
        return OperationResult<Brand>.Ok(brand);
    }

    public async Task<OperationResult<Brand>> UpdateBrandAsync(Brand brand)
    {
        var existingBrand = await _dbContext.Brands.FindAsync(brand.Id) ?? throw new InvalidOperationException($"Brand with ID {brand.Id} not found.");
        existingBrand.BrandName = brand.BrandName;
        existingBrand.IsDeleted = brand.IsDeleted;

        await _dbContext.SaveChangesAsync();
        return OperationResult<Brand>.Ok(existingBrand);
    }

    public async Task<OperationResult<Brand>> AddBrandAsync(Brand brand)
    {
        await _dbContext.Brands.AddAsync(brand);
        await _dbContext.SaveChangesAsync();
        return OperationResult<Brand>.Ok(brand);
    }
}
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repository;

public class BrandRepository : IBrandRepository
{
    private readonly ProductDbContext _dbContext;
    public async Task<PageResult<Brand>> GetAllBrandsAsync(PageRequest pageRequest)
    {
        var totalRecords = await _dbContext.Brands.CountAsync();

        var brands = await _dbContext.Brands
            .Skip((pageRequest.Page - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .ToListAsync();  
        return new PageResult<Brand>(brands, totalRecords, pageRequest.Page, pageRequest.Size);
    }

    public async Task<int> CountAllBrandsAsync()
    {
        return await _dbContext.Brands.CountAsync();
    }

    public async Task<PageResult<Brand>> GetAllItemBrandsAsync(PageRequest pageRequest, Guid brandId)
    {
        var totalRecords = _dbContext.Brands.Count(b => b.Id == brandId);
        var brands = await _dbContext.Brands
            .Include(b => b.Products)
            .Where(b => b.Id == brandId)
            .Skip((pageRequest.Page - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .ToListAsync();
        return new PageResult<Brand>(brands, totalRecords, pageRequest.Page, pageRequest.Size);
    }

    public async Task<Brand> GetBrandAsync(Guid brandId)
    {
        var brand = await _dbContext.Brands.FindAsync(brandId);
        if (brand == null)
        {
            throw new InvalidOperationException($"Brand with ID {brandId} not found.");
        }
        return brand;
    }

    public async Task<Brand> SaveBrandAsync(Brand brand)
    {
        _dbContext.Brands.Add(brand);
        await _dbContext.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> DeleteBrandAsync(Guid brandId)
    {
        var brand = await _dbContext.Brands.FindAsync(brandId);
        if (brand == null)
        {
            throw new InvalidOperationException($"Brand with ID {brandId} not found.");
        }
        brand.IsDeleted = true;
        await _dbContext.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> UpdateBrandAsync(Brand brand)
    {
        var existingBrand = await _dbContext.Brands.FindAsync(brand.Id);
        if (existingBrand == null)
        {
            throw new InvalidOperationException($"Brand with ID {brand.Id} not found.");
        }

        existingBrand.BrandName = brand.BrandName;
        existingBrand.IsDeleted = brand.IsDeleted;

        await _dbContext.SaveChangesAsync();
        return existingBrand;
    }
}
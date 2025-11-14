using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repository;

public class ProductRepository(ProductDbContext context) : IProductRepository
{
    public async Task<PageResult<Product>> GetAllAsync(PageRequest pageRequest)
    {
        var totalCount = await context.Products.CountAsync();

        var products = await context.Products
            .Skip(pageRequest.Skip)
            .Take(pageRequest.Size)
            .Include(b => b.Brand)
            .ToListAsync();
        return new PageResult<Product>(products, totalCount, pageRequest.Page, pageRequest.Size);
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        var item = await context.Products
            .Include(b => b.Brand)
            .Include(i => i.Images)
            .FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
        return item ?? null;
    }

    public async Task<Product> SaveAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }


    public async Task<bool> DeleteAsync(string id)
    {
        var product = await context.Products.FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
        if (product == null) return false;
        product.IsDeleted = true;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<PageResult<Product>> GetAllProductsWithKeywordAsync(PageRequest pageRequest, string keyword)
    {
        var productsQuery = await context.Products
            .Where(p => p.IsDeleted == false &&
            (EF.Functions.Contains(p.ProductName, keyword) ||
            EF.Functions.Contains(p.ProductDescription, keyword))).ToListAsync();

        var totalCount = productsQuery.Count;
        var products = productsQuery
            .Skip(pageRequest.Skip)
            .Take(pageRequest.Size)
            .ToList();
        return new PageResult<Product>(products, totalCount, pageRequest.Page, pageRequest.Size);
    }

    public async Task<Product?> AddProductAsync(Product product)
    {
        try
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            var result =  context.Products.Where(c => c.Id == product.Id).Include(b => b.Brand).Include(i => i.Images);
            return result.FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
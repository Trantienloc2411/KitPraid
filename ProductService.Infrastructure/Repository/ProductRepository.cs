using Microsoft.EntityFrameworkCore;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repository;

public class ProductRepository(ProductDbContext context) : IProductRepository
{
    public async Task<OperationResult<PageResult<Product>>> GetAllAsync(PageRequest pageRequest)
    {
        var totalCount = await context.Products.CountAsync();

        var products = await context.Products
            .Skip(pageRequest.Skip)
            .Take(pageRequest.Size)
            .Include(b => b.Brand)
            .Include(i => i.Images)
            .OrderByDescending(p => p.Created)
            .ToListAsync();

        var page = new PageResult<Product>(products, totalCount, pageRequest.Page, pageRequest.Size);
        return OperationResult<PageResult<Product>>.Ok(page);
    }

    public async Task<OperationResult<Product>> GetByIdAsync(string id)
    {
        if (!Guid.TryParse(id, out var guid))
        {
            return OperationResult<Product>.Fail("Invalid product id format.");
        }

        var item = await context.Products
            .Include(b => b.Brand)
            .Include(i => i.Images)
            .FirstOrDefaultAsync(c => c.Id == guid);

        return item is null
            ? OperationResult<Product>.Fail($"Product with ID {id} not found.")
            : OperationResult<Product>.Ok(item);
    }

    public async Task<OperationResult<Product>> SaveAsync(Product product)
    {

            context.Products.Add(product);
            await context.SaveChangesAsync();
            return OperationResult<Product>.Ok(product);

    }

    public async Task<OperationResult<bool>> DeleteAsync(string id)
    {

            if (!Guid.TryParse(id, out var guid))
            {
                return OperationResult<bool>.Fail("Invalid product id format.");
            }

            var product = await context.Products.FirstOrDefaultAsync(c => c.Id == guid);
            if (product is null)
            {
                return OperationResult<bool>.Fail($"Product with ID {id} not found.");
            }

            product.IsDeleted = true;
            await context.SaveChangesAsync();
            return OperationResult<bool>.Ok(true);
        
    }

    public async Task<OperationResult<PageResult<Product>>> GetAllProductsWithKeywordAsync(
        PageRequest pageRequest, string keyword)
    {
        var productsQuery = context.Products
            .Where(p => p.IsDeleted == false && p.IsActive == true &&
                        (p.ProductName.Contains(keyword) ||
                         p.ProductDescription.Contains(keyword) ||
                         p.Sku.Contains(keyword)))
            .Include(p => p.Brand)
            .Include(p => p.Images);

        var totalCount = await productsQuery.CountAsync();

        var products = await productsQuery
            .Skip(pageRequest.Skip)
            .Take(pageRequest.Size)
            .ToListAsync();

        var page = new PageResult<Product>(products, totalCount, pageRequest.Page, pageRequest.Size);

        return OperationResult<PageResult<Product>>.Ok(page);
    }


    public async Task<OperationResult<Product>> AddProductAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        var result = await context.Products
            .Where(c => c.Id == product.Id)
            .Include(b => b.Brand)
            .Include(i => i.Images)
            .FirstOrDefaultAsync();

        if (result is null)
        {
            return OperationResult<Product>.Fail($"Product with ID {product.Id} not found after creation.");
        }

        return OperationResult<Product>.Ok(result);
    }

    public async Task<OperationResult<Product>> UpdateAsync(Product product)
    {
        var result = context.Products.Update(product);
        var saveResult = await context.SaveChangesAsync();
        return OperationResult<Product>.Ok(product);
    }
}
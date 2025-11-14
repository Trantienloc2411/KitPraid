using System;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Dtos;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;
using ProductService.Infrastructure.Mapping;

namespace ProductService.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<OperationResult<Product>> CreateProductAsync(CreateProductDto product)
    {
        try
        {
            var mapping = ProductMapper.ToCreateProduct(product);
            var result = await productRepository.AddProductAsync(mapping);
            return result;
        }
        catch (DbUpdateException e)
        {
            return OperationResult<Product>.Fail(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return OperationResult<Product>.Fail($"Details : {e.Message}");
        }
        catch (ArgumentException e)
        {
            return OperationResult<Product>.Fail($"Details : {e.Message}");
        }
        catch (Exception e)
        {
            return OperationResult<Product>.Fail($"Some unexpected error. Details : {e.Message}");
        }
    }

    public async Task<OperationResult<bool>> DeleteProductAsync(string id)
    {
        try
        {
            var result = await productRepository.DeleteAsync(id);
            return result;
        }
        catch (Exception e)
        {
            return OperationResult<bool>.Fail($"Some unexpected error. Details : {e.Message}");
        }
    }

    public async Task<OperationResult<PageResult<Product>>> GetAllProductsAsync(PageRequest pageRequest)
    {
        try
        {
            return await productRepository.GetAllAsync(pageRequest);
        }
        catch (Exception e)
        {
            return OperationResult<PageResult<Product>>.Fail($"Some unexpected error. Details : {e.Message}");
        }
    }

    public Task<OperationResult<Product>> GetProductByIdAsync(string id)
    {
        return productRepository.GetByIdAsync(id);
    }

    public async Task<OperationResult<Product>> UpdateProductAsync(string id, UpdateProductDto product)
    {
        var productOp = await productRepository.GetByIdAsync(id);
        if (productOp.Success)
        {
            if (productOp.Data != null) ProductMapper.ApplyUpdate(productOp.Data, product);
            else return OperationResult<Product>.Fail("Product not found");
        }
        if (productOp.Error != null)
            return OperationResult<Product>.Fail(productOp.Error);

        if (productOp.Data != null)
        {
            var update = await productRepository.UpdateAsync(productOp.Data);
            return update;
        }
        else return OperationResult<Product>.Fail("Update error!");
    }
}

using System;
using Meilisearch;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Dtos;
using ProductService.Application.Mapping;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;

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

    public async Task<OperationResult<PageResult<GetProductDto>>> GetProductsByKeywordAsync(PageRequest pageRequest, string keyword)
    {
        try
        {
            var result = await productRepository.GetAllProductsWithKeywordAsync(pageRequest, keyword);
            if (result.Success)
            {
                var mappedItems = result.Data.Items
                    .Select(ProductMapper.ToGetProductDto)
                    .ToList();
                var pageResult = new PageResult<GetProductDto>(
                    mappedItems,
                    result.Data.TotalCount,
                    pageRequest.Page,
                    pageRequest.Size
                );
                return OperationResult<PageResult<GetProductDto>>.Ok(pageResult);
            }
            else
            {
                return OperationResult<PageResult<GetProductDto>>.Fail(result.Error ?? "Unknown error");
            }
            
            
            /*var client = new MeilisearchClient("http://localhost:7700", "ttloc2411");
            
            var index = await client.Index("products")
                .UpdateSettingsAsync(
                    new Settings
                    {
                        SearchableAttributes = new[] { "ProductName", "ProductDescription", "Sku" },
                        FilterableAttributes = new[] { "Price" },
                        SortableAttributes = new [] {"Price"}
                    });*/
        }
        catch (DbUpdateException e)
        {
            return OperationResult<PageResult<GetProductDto>>.Fail(e.Message);
        }
    }

    public async Task<OperationResult<PageResult<GetProductDto>>> GetAllProductsAsync(PageRequest pageRequest)
    {
        try
        {
           
            var result = await productRepository.GetAllAsync(pageRequest);
            var mappedItems = result.Data.Items
                .Select(ProductMapper.ToGetProductDto)
                .ToList();
            var pageResult = new PageResult<GetProductDto>(
                mappedItems,
                result.Data.TotalCount,
                pageRequest.Page,
                pageRequest.Size
            );
            return OperationResult<PageResult<GetProductDto>>.Ok(pageResult);
        }
        catch (Exception e)
        {
            return OperationResult<PageResult<GetProductDto>>.Fail(
                $"Some unexpected error. Details: {e.Message}"
            );
        }
    }


    public async Task<OperationResult<GetProductDetailDto>> GetProductByIdAsync(string id)
    {
        var result = await productRepository.GetByIdAsync(id);
        if (result.Success)
        {
            if (result.Data != null)
            {
                var mapped = ProductMapper.ToGetProductDetailDto(result.Data);
                return OperationResult<GetProductDetailDto>.Ok(mapped);
            }
            else
            {
                return OperationResult<GetProductDetailDto>.Fail("Product not found");
            }
        }
        else
        {
            return OperationResult<GetProductDetailDto>.Fail(result.Error ?? "Unknown error");
        }
       
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

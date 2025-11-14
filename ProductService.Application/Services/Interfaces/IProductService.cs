using System;
using ProductService.Application.Dtos;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Application.Services;

public interface IProductService
{
    public Task<OperationResult<PageResult<Product>>> GetAllProductsAsync(PageRequest pageRequest);
    public Task<OperationResult<Product>> GetProductByIdAsync(string id);
    public Task<OperationResult<Product>> UpdateProductAsync(string id, UpdateProductDto product);
    public Task<OperationResult<Product>> CreateProductAsync(CreateProductDto product);
    public Task<OperationResult<bool>> DeleteProductAsync(string id);

}

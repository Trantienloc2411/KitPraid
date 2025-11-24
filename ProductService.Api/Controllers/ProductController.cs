using IdentityServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Dtos;
using ProductService.Application.Services;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Api.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController(IProductService productService, ILogger<ProductController> logger) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OperationResult<PageResult<Product>>>> Get([FromBody]PageRequestDto request)
    {
        logger.LogInformation("Start Process");
        var pr = new PageRequest(request.Page, request.Size);
        var result = await productService.GetAllProductsAsync(pr);
    
        if (!result.Success)
        {
            return BadRequest(result);
        }
    
        return Ok(result);
    }

    [HttpGet("{keyword}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<ActionResult<OperationResult<Product>>> GetWithKeyWord(
        [FromRoute] string keyword,
        [FromQuery] PageRequestDto request)
    {
        var pr = new PageRequest(request.Page, request.Size);
        logger.LogInformation("Start Process");
        var result = await productService.GetProductsByKeywordAsync(pr, keyword);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    
    [HttpPost]
    [Authorize("Admin")]
    public async Task<ActionResult<OperationResult<Product>>> Create([FromBody]CreateProductDto product)
    {
        var result = await productService.CreateProductAsync(product);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OperationResult<Product>>> Update(string id, [FromBody] UpdateProductDto product)
    {
        var result = await productService.UpdateProductAsync(id, product);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationResult<bool>>> Delete(string id)
    {
        var result = await productService.DeleteProductAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }


    
}
using IdentityServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<OperationResult<PageResult<Product>>>> Get([FromBody]PageRequest request)
    {
        logger.LogInformation("Start Process");
        var result = await productService.GetAllProductsAsync(request);
    
        if (!result.Success)
        {
            return BadRequest(result);
        }
    
        return Ok(result);
    }

    
}
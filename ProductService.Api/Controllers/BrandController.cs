using IdentityServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Dtos;
using ProductService.Application.Services.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Api.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class BrandController(IBrandService brandService, ILogger<BrandController> logger) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin, Customer, User")]
    public async Task<ActionResult<OperationResult<Brand>>> GetAllBrandsAsync([FromBody] PageRequestDto request)
    {
        var pageRequest = new PageRequest(request.Page, request.Size);
        var brands = await brandService.GetAllBrandsAsync(pageRequest);
        return Ok(brands);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Customer, User")]
    public async Task<ActionResult<OperationResult<Brand>>> GetBrandAsync(Guid id)
    {
        var brand = await brandService.GetBrandAsync(id);
        return Ok(brand);
    }
    
    [HttpGet("{id}/items")]
    [Authorize(Roles = "Admin, Customer, User")]
    public async Task<ActionResult<OperationResult<PageResult<GetBrandItemsDto>>>> GetAllItemBrandsAsync(Guid id, [FromQuery] PageRequestDto request)
    {
        var pageRequest = new PageRequest(request.Page, request.Size);
        var brands = await brandService.GetAllItemBrandsAsync(pageRequest, id);
        return Ok(brands);
    }
    
    [HttpPost]
    [Authorize("Admin")]
    public async Task<ActionResult<OperationResult<Brand>>> CreateBrandAsync([FromBody] CreateBrandDto brand)
    {
        var result = await brandService.CreateBrandAsync(brand);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id}")]
    [Authorize("Admin")]
    public async Task<ActionResult<OperationResult<UpdateBrandDto>>> UpdateBrandAsync(Guid id,
        [FromBody] UpdateBrandDto brand)
    {
        var result = await brandService.UpdateBrandAsync(id.ToString(), brand);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    [Authorize("User")]
    public async Task<ActionResult<OperationResult<GetBrandDto>>> DeleteBrandAsync(Guid id)
    {
        var result = await brandService.DeleteBrandAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
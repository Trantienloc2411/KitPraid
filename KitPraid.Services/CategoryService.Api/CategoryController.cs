using CategoryService.Application.Dtos;
using CategoryService.Application.Services.Interfaces;
using CategoryService.Domain;
using CategoryService.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CategoryService.Api;
[ApiController]
[Route("[controller]")]
[Authorize]
public class CategoryController(ICategoryService service, ILogger<CategoryController> logger) : ControllerBase
{
    [HttpGet("tree")]
    [Authorize(Roles = "Admin, Customer")]
    public async Task<ActionResult<OperationResult<CategoryTreeDto>>> GetCategory()
    {
        var result = await service.GetAllTreeAsync();
        return Ok(result);
    }

    [HttpGet()]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OperationResult<List<CategoryDto>>>> GetAll([FromBody] PageRequest pageRequest)
    {
        var result = await service.GetAllAsync(pageRequest);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin, Customer")]
    public async Task<ActionResult<OperationResult<CategoryDto>>> GetById(Guid id)
    {
        var result = await service.GetByIdAsync(id);
        return Ok(result);  
    }
    
    [HttpPost()]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OperationResult<CategoryDto>>> Create([FromBody] CreateCategoryDto category)
    {
        var result = await service.CreateAsync(category);
        return Ok(result);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OperationResult<CategoryDto>>> Update(Guid id, [FromBody] UpdateCategoryDto updateCategoryDto)
    {
        var result = await service.UpdateAsync(id, updateCategoryDto);
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OperationResult<bool>>> Delete(Guid id)
    {
        var result = await service.DeleteAsync(id);
        return Ok(result);
    }
    
    
    
}
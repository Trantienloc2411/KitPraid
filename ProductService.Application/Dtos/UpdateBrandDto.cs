namespace ProductService.Application.Dtos;

public class UpdateBrandDto
{
    public string? BrandName { get; set; }
    public string? Description { get; set; }
    public string? BrandImageUrl { get; set; }
    public bool IsActive { get; set; }
}
namespace ProductService.Application.Dtos;

public class CreateBrandDto
{
    //If user doesn't provide brand code, we generate one based on BrandName [BrandName.Trim().ToUpper().Substring(0,4)+[random number]]
    public string BrandCode { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? BrandImageUrl { get; set; }
    public bool IsActive { get; set; }
}
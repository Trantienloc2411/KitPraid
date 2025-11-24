namespace ProductService.Application.Dtos;

public class GetBrandItemsDto
{
    public Guid BrandId { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public List<GetProductDto> Products { get; set; } = new List<GetProductDto>();
}
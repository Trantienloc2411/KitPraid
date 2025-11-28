namespace ProductService.Application.Dtos;

public class UpdateProductDto
{
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public Guid? BrandId { get; set; }
    public int Stock { get; set; }
    public Dictionary<string, object?>? Attributes { get; set; }
    public double Price { get; set; }
    public string? CategoryId { get; set; }
    public bool? IsActive { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;

}
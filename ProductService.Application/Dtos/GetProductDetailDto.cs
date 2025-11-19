using ProductService.Domain.Entities;
namespace ProductService.Application.Dtos
{
    public class GetProductDetailDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public string? CategoryId { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public Dictionary<string, object?>? Attributes { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        // Brand DTO only
        public GetBrandDto? BrandDto { get; set; }
    }
}

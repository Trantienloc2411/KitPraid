using System.Text.Json;
using System.Text.Json.Serialization;
using ProductService.Domain.Entities;

namespace ProductService.Application.Dtos;

public class GetProductDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public int Stock { get; set; }
    [JsonIgnore]
    private string? AttributesJson { get; set; }
    public Dictionary<string, object?>? Attributes 
    {  get => string.IsNullOrEmpty(AttributesJson)
            ? new Dictionary<string, object?>()
            : JsonSerializer.Deserialize<Dictionary<string, object?>>(AttributesJson)!;
        set => AttributesJson = JsonSerializer.Serialize(value);
        
    }
    
    public double Price { get; set; }
    public string? CategoryId { get; set; }
    public List<Image>? Images { get; set; } = null;
    public string? BrandName { get; set; } = string.Empty;
    
}
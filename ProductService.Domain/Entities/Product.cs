using System.ComponentModel;
using System.Text.Json;

namespace ProductService.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public required string ProductName { get; set; }
    public required string ProductDescription { get; set; }
    public Guid BrandId { get; set; }
    public required string Sku { get; set; }
    public int Stock { get; set; }

    // Backing JSON column
    public string? AttributesJson { get; set; }


    public Dictionary<string, object?>? Attributes
    {
        get => string.IsNullOrEmpty(AttributesJson)
            ? new Dictionary<string, object?>()
            : JsonSerializer.Deserialize<Dictionary<string, object?>>(AttributesJson)!;
        set => AttributesJson = JsonSerializer.Serialize(value);
    }

    [DefaultValue(0.0)]
    public double Price { get; set; } = 0.0m;

    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public virtual ICollection<Image>? Images { get; set; }
    public virtual Brand? Brand { get; set; }

    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public Guid UserId { get; set; }
}
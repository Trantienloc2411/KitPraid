using System.Text.Json.Serialization;

namespace ProductService.Domain.Entities;

public class Image
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public required string ImageName { get; set; }
    public required string ImagePath { get; set; }
    [JsonIgnore]
    public Guid ProductId { get; set; }
    
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; }
    
}
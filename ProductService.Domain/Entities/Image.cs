using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductService.Domain.Entities;

public class Image
{
    [JsonIgnore]
    public Guid Id { get; set; }
    [Required]
    public required string ImageName { get; set; }
    [Required]
    public required string ImagePath { get; set; }
    [JsonIgnore]
    public Guid ProductId { get; set; }
    
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; }
    
}
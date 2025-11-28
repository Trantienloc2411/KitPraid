using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductService.Domain.Entities;

public class Brand
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(20)]
    public required string BrandCode {get; set;}
    [Required]
    [StringLength(50)]
    public required string BrandName {get; set;}
    public required string BrandDescription {get; set;}
    [Required]
    [Url]
    public required string BrandImage {get; set;}
    public DateTime DateCreated {get; set;}
    public DateTime DateModified {get; set;}
    
    public bool IsActive {get; set;}
    public bool IsDeleted {get; set;}
    [JsonIgnore]
    public virtual ICollection<Product>? Products {get; set;}
    
}
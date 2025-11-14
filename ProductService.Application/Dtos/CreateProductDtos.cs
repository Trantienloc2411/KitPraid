using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ProductService.Domain.Entities;

namespace ProductService.Application.Dtos;

public class CreateProductDtos
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(5, ErrorMessage = "At least 5 chars"), MaxLength(120, ErrorMessage = "At most 120 chars")]
    public string ProductName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Description is required")]
    [MinLength(20, ErrorMessage = "At least 20 chars"), MaxLength(350,  ErrorMessage = "At most 350 chars")]
    public string ProductDescription { get; set; } = string.Empty;
    [Required(ErrorMessage = "Price is required")]
    [Range(1, 99999999999)]
    [DataType(DataType.Currency, ErrorMessage = "Price must be between 1 and 99999999999")]
    public double Price { get; set; }
    [Required(ErrorMessage = "BrandId is required")]
    public Guid BrandId { get; set; }
    [Required(ErrorMessage = "Sku is required")]
    public string Sku { get; set; } =  string.Empty;
    [Required(ErrorMessage = "Attributes is required")]
    public Dictionary<string, object>? Attributes { get; set; }
    [DefaultValue(false)]
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; } = false;
    public ICollection<Image>? Images { get; set; }
    public Guid UserId { get; set; }
    
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Modified { get; set; } = DateTime.Now;
    
}
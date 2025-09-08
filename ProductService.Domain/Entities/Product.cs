namespace ProductService.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public required string ProductName { get; set; }
    public required string ProductDescription { get; set; }
    public Guid BrandId { get; set; }
    public required string Sku { get; set; }
    public int Stock { get; set; }
    public Dictionary<string, object?>? Attributes { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public virtual ICollection<Image>? Images { get; set; }
    public virtual Brand? Brand { get; set; }
    
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    
    public Guid UserId { get; set; }
    
    
    
}
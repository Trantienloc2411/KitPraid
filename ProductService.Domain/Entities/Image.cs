namespace ProductService.Domain.Entities;

public class Image
{
    public Guid Id { get; set; }
    public required string ImageName { get; set; }
    public required string ImagePath { get; set; }
    public Guid ProductId { get; set; }
    
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Product? Product { get; set; }
    
}
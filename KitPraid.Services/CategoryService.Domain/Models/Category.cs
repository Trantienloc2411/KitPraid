using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CategoryService.Domain.Models;

public class Category
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(35)]
    public string CategoryName { get; set; } = string.Empty;
    [MaxLength(250)]
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    [AllowNull]
    public DateTime Modified { get; set; }
    
    public Guid? ParentCategoryId { get; set; }
    
    [ForeignKey(nameof(ParentCategoryId))]
    public virtual Category? ParentCategory { get; set; }
    
    public virtual ICollection<Category>? ChildCategories { get; set; } = new List<Category>();
}
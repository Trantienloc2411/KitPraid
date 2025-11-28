using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CategoryService.Application.Dtos;

public class UpdateCategoryDto
{
    [Required]
    [NotNull]
    [MinLength(2), MaxLength(35)]
    public string? CategoryName { get; set; }
    [MinLength(2), MaxLength(300)]
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public bool IsActive { get; set; }
}
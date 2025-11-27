namespace CategoryService.Application.Dtos;

public class CreateCategoryDto
{
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public bool IsActive { get; set; } = true;
}
namespace CategoryService.Application.Dtos;

public class CategoryTreeDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    
    public Guid? ParentCategoryId { get; set; }
    public List<CategoryTreeDto>? ChildCategories { get; set; } = [];
}
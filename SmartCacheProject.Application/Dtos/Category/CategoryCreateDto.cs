namespace SmartCacheProject.Application.Dtos.Category;

public class CategoryCreateDto
{
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    public bool IsActive { get; set; }
}

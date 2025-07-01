using SmartCacheProject.Domain.Entities.Common;

namespace SmartCacheProject.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; } 
    public DateTime LastModified { get; set; }
    public bool IsActive { get; set; }
}

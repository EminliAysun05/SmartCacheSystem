using SmartCacheProject.Domain.Entities.Common;

namespace SmartCacheProject.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime LastModified { get; set; }
    public bool IsActive { get; set; }
}

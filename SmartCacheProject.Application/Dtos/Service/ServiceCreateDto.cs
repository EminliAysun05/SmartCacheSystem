namespace SmartCacheProject.Application.Dtos.Service;

public class ServiceCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}

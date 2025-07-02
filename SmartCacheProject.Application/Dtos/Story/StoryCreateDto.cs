namespace SmartCacheProject.Application.Dtos.Story;

public class StoryCreateDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? ImageUrl { get; set; } = null!;
    public bool IsPublished { get; set; }
}

using SmartCacheProject.Domain.Dtos.Story;

namespace SmartCacheProject.Application.Services.Interfaces;

public interface IStoryService
{
    Task<List<StoryResponseDto>> GetAllAsync();
    Task<StoryResponseDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(StoryCreateDto dto);
    Task<bool> UpdateAsync(StoryUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

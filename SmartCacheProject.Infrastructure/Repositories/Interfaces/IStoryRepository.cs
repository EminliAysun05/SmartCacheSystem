using SmartCacheProject.Domain.Entities;

namespace SmartCacheProject.Infrastructure.Repositories.Interfaces;

public interface IStoryRepository
{
    Task<List<Story>> GetAllAsync();
    Task<Story?> GetByIdAsync(int id);
    Task AddAsync(Story story);
    Task Update(Story story);
    Task Delete(int id);
    Task<DateTime?> GetLastModifiedAsync();
}

using SmartCacheProject.Domain.Entities;

namespace SmartCacheProject.Persistence.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category?>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task Update(Category category);
        Task Delete(int id);
        Task<DateTime?> GetLastModifiedAsync();
    }
}

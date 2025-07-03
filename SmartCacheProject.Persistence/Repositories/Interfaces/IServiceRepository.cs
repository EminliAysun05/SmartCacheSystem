using SmartCacheProject.Domain.Entities;

namespace SmartCacheProject.Persistence.Repositories.Interfaces;

public interface IServiceRepository
{
    Task<List<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(int id);
    Task AddAsync(Service service);
    Task Update(Service service);
    Task Delete(int id);
    Task<DateTime?> GetLastModifiedAsync();
}

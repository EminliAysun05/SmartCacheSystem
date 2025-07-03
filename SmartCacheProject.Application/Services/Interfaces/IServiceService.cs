using SmartCacheProject.Domain.Dtos.Service;

namespace SmartCacheProject.Application.Services.Interfaces;

public interface IServiceService
{
    Task<List<ServiceResponseDto>> GetAllAsync();
    Task<ServiceResponseDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(ServiceCreateDto dto);
    Task<bool> UpdateAsync(ServiceUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

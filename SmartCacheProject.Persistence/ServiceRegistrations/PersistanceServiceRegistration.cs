using Microsoft.Extensions.DependencyInjection;
using SmartCacheProject.Persistence.Repositories;
using SmartCacheProject.Persistence.Repositories.Implementations;
using SmartCacheProject.Persistence.Repositories.Interfaces;

namespace SmartCacheProject.Persistence.ServiceRegistrations;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
      
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IStoryRepository, StoryRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();

        return services;
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartCacheProject.Application.Services.Interfaces;
using SmartCacheProject.Infrastructure.Caching;
using SmartCacheProject.Infrastructure.Caching.Interfaces;
using SmartCacheProject.Infrastructure.Services;
using StackExchange.Redis;

namespace SmartCacheProject.Infrastructure.ServiceRegistrations;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
        services.AddSingleton<IConnectionMultiplexer>(redis);

        services.AddScoped<IRedisCacheService, RedisCacheService>();

        services.AddScoped<IChangeDetectionService, ChangeDetectionService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IStoryService, StoryService>();
        services.AddScoped<IUserProfileService, UserProfileService>();

        services.AddScoped<IDatabase>(sp =>
        {
            var multiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
            return multiplexer.GetDatabase();
        });

        return services;
    }
}

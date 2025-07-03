using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartCacheProject.Infrastructure.Caching;
using SmartCacheProject.Infrastructure.Caching.Interfaces;
using StackExchange.Redis;

namespace SmartCacheProject.Infrastructure.ServiceRegistrations;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisHost = configuration["Redis:Host"] ?? "localhost";
            var redisPort = configuration["Redis:Port"] ?? "6379";
            var configString = $"{redisHost}:{redisPort}";
            return ConnectionMultiplexer.Connect(configString);
        });

        services.AddScoped<IRedisCacheService, RedisCacheService>();

     

        return services;
    }
}

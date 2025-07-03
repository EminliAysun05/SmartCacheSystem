using Microsoft.Data.SqlClient;
using SmartCacheProject.Application.ServiceRegistrations;
using SmartCacheProject.Infrastructure.ServiceRegistrations;
using SmartCacheProject.Migrations.MigrationServiceRegistrations;
using SmartCacheProject.Persistence.ServiceRegistrations;

namespace SmartCacheProject.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMigrationDbContext(builder.Configuration);
        builder.Services.AddPersistenceServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        
        var app = builder.Build();
       
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}


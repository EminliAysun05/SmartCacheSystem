
using System;
using Microsoft.EntityFrameworkCore;
using SmartCacheProject.Domain.Entities;
using SmartCacheProject.Migrations;
using SmartCacheProject.Migrations.MigrationServiceRegistrations;


namespace SmartCacheProject.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
         builder.Services.AddMigrationDbContext(builder.Configuration);
     //   builder.Services.AddDbContext<AppDbContext>(options =>
     //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        // Configure the HTTP request pipeline.
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

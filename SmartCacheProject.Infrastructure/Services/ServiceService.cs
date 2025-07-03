using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SmartCacheProject.Application.Services.Interfaces;
using SmartCacheProject.Domain.Dtos.Service;
using SmartCacheProject.Domain.Entities;

namespace SmartCacheProject.Infrastructure.Services;

public class ServiceService : IServiceService
{
    private readonly string _connectionString;
    private readonly IMapper _mapper;

    public ServiceService(IConfiguration configuration, IMapper mapper)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        _mapper = mapper;
    }
    public async Task<int> CreateAsync(ServiceCreateDto dto)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand(@"
                INSERT INTO Services (Name, Description, Price, LastModified, IsActive)
                VALUES (@Name, @Description, @Price, @LastModified, @IsActive);
                SELECT SCOPE_IDENTITY();", connection);

        command.Parameters.AddWithValue("@Name", dto.Name);
        command.Parameters.AddWithValue("@Description", (object?)dto.Description ?? DBNull.Value);
        command.Parameters.AddWithValue("@Price", dto.Price);
        command.Parameters.AddWithValue("@LastModified", DateTime.UtcNow);
        command.Parameters.AddWithValue("@IsActive", true);

        await connection.OpenAsync();
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("DELETE FROM Services WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();
        var affected = await command.ExecuteNonQueryAsync();
        return affected > 0;
    }

    public async Task<List<ServiceResponseDto>> GetAllAsync()
    {
        var result = new List<Service>();
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM Services", connection);
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(new Service
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                Price = reader.GetDecimal(3),
                LastModified = reader.GetDateTime(4),
                IsActive = reader.GetBoolean(5)
            });
        }
        return _mapper.Map<List<ServiceResponseDto>>(result);
    }

    public async Task<ServiceResponseDto?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM Services WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var service = new Service
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                Price = reader.GetDecimal(3),
                LastModified = reader.GetDateTime(4),
                IsActive = reader.GetBoolean(5)
            };
            return _mapper.Map<ServiceResponseDto>(service);
        }
        return null;
    }

    public async Task<bool> UpdateAsync(ServiceUpdateDto dto)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand(@"
                UPDATE Services SET 
                    Name = @Name,
                    Description = @Description,
                    Price = @Price,
                    LastModified = @LastModified,
                    IsActive = @IsActive
                WHERE Id = @Id", connection);

        command.Parameters.AddWithValue("@Id", dto.Id);
        command.Parameters.AddWithValue("@Name", dto.Name);
        command.Parameters.AddWithValue("@Description", (object?)dto.Description ?? DBNull.Value);
        command.Parameters.AddWithValue("@Price", dto.Price);
        command.Parameters.AddWithValue("@LastModified", DateTime.UtcNow);
        command.Parameters.AddWithValue("@IsActive", dto.IsActive);

        await connection.OpenAsync();
        var affected = await command.ExecuteNonQueryAsync();
        return affected > 0;
    }

    
}

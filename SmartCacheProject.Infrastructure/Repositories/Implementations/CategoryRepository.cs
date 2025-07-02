using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SmartCacheProject.Domain.Entities;
using SmartCacheProject.Infrastructure.Repositories.Interfaces;

namespace SmartCacheProject.Infrastructure.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly IConfiguration _configuration;

    public CategoryRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private SqlConnection GetConnection() => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    public async Task AddAsync(Category category)
    {
        var query = @"
            INSERT INTO Categories (Name, ParentId, LastModified, IsActive)
            VALUES (@Name, @ParentId, @LastModified, @IsActive)";

        using var connection = GetConnection();
        await connection.OpenAsync();

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", category.Name);
        command.Parameters.AddWithValue("@ParentId", (object?)category.ParentId ?? DBNull.Value);
        command.Parameters.AddWithValue("@LastModified", category.LastModified);
        command.Parameters.AddWithValue("@IsActive", category.IsActive);

        await command.ExecuteNonQueryAsync();

    }

    public async Task Delete(int id)
    {
        var query = "DELETE FROM Categories WHERE Id = @Id";

        using var connection = GetConnection();
        await connection.OpenAsync();

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<Category?>> GetAllAsync()
    {
        var categories = new List<Category?>();
        var query = "SELECT Id, Name, ParentId, LastModified, IsActive FROM Categories";

        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            categories.Add(new Category
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                ParentId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                LastModified = reader.GetDateTime(3),
                IsActive = reader.GetBoolean(4)
            });
        }
        return categories;
    }
    public async Task<Category?> GetByIdAsync(int id)
    {
        var query = "SELECT Id, Name, ParentId, LastModified, IsActive FROM Categories WHERE Id = @Id";

        using var connection = GetConnection();
        await connection.OpenAsync();

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Category
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                ParentId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                LastModified = reader.GetDateTime(3),
                IsActive = reader.GetBoolean(4)
            };
        }

        return null;
    }

    public async Task<DateTime?> GetLastModifiedAsync()
    {
       var query = "SELECT MAX(LastModified) FROM Categories";

        using var connection = GetConnection();
        await connection.OpenAsync();

        using var command = new SqlCommand(query, connection);
        var result = await command.ExecuteScalarAsync();

        return result is DBNull or null ? null : Convert.ToDateTime(result);
    }

    public async Task Update(Category category)
    {
        var query = @"
            UPDATE Categories
            SET Name = @Name,
                ParentId = @ParentId,
                LastModified = @LastModified,
                IsActive = @IsActive
            WHERE Id = @Id";
        
        using var connection = GetConnection();
        await connection.OpenAsync();

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", category.Name);
        command.Parameters.AddWithValue("@ParentId", (object?)category.ParentId ?? DBNull.Value);
        command.Parameters.AddWithValue("@LastModified", category.LastModified);
        command.Parameters.AddWithValue("@IsActive", category.IsActive);
        command.Parameters.AddWithValue("@Id", category.Id);

        await command.ExecuteNonQueryAsync();

    }
}

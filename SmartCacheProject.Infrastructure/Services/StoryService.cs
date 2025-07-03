using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SmartCacheProject.Application.Services.Interfaces;
using SmartCacheProject.Domain.Dtos.Story;
using SmartCacheProject.Domain.Entities;

public class StoryService : IStoryService
{
    private readonly string _connectionString;
    private readonly IMapper _mapper;

    public StoryService(IConfiguration configuration, IMapper mapper)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        _mapper = mapper;
    }

    public async Task<List<StoryResponseDto>> GetAllAsync()
    {
        var result = new List<Story>();
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM Stories", connection);
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(new Story
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Content = reader.GetString(2),
                ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3),
                LastModified = reader.GetDateTime(4),
                IsPublished = reader.GetBoolean(5)
            });
        }
        return _mapper.Map<List<StoryResponseDto>>(result);
    }

    public async Task<StoryResponseDto?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM Stories WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var story = new Story
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Content = reader.GetString(2),
                ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3),
                LastModified = reader.GetDateTime(4),
                IsPublished = reader.GetBoolean(5)
            };
            return _mapper.Map<StoryResponseDto>(story);
        }
        return null;
    }

    public async Task<int> CreateAsync(StoryCreateDto dto)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand(@"
        INSERT INTO Stories (Title, Content, ImageUrl, LastModified, IsPublished)
        VALUES (@Title, @Content, @ImageUrl, @LastModified, @IsPublished);
        SELECT SCOPE_IDENTITY();", connection);

        command.Parameters.AddWithValue("@Title", dto.Title);
        command.Parameters.AddWithValue("@Content", dto.Content);
        command.Parameters.AddWithValue("@ImageUrl", (object?)dto.ImageUrl ?? DBNull.Value);
        command.Parameters.AddWithValue("@LastModified", DateTime.UtcNow);
        command.Parameters.AddWithValue("@IsPublished", false); // default policy

        await connection.OpenAsync();
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

    public async Task<bool> UpdateAsync(StoryUpdateDto dto)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand(@"
            UPDATE Stories SET 
                Title = @Title,
                Content = @Content,
                ImageUrl = @ImageUrl,
                LastModified = @LastModified,
                IsPublished = @IsPublished
            WHERE Id = @Id", connection);

        command.Parameters.AddWithValue("@Id", dto.Id);
        command.Parameters.AddWithValue("@Title", dto.Title);
        command.Parameters.AddWithValue("@Content", dto.Content);
        command.Parameters.AddWithValue("@ImageUrl", (object?)dto.ImageUrl ?? DBNull.Value);
        command.Parameters.AddWithValue("@LastModified", DateTime.UtcNow);
        command.Parameters.AddWithValue("@IsPublished", dto.IsPublished);

        await connection.OpenAsync();
        var affected = await command.ExecuteNonQueryAsync();
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("DELETE FROM Stories WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();
        var affected = await command.ExecuteNonQueryAsync();
        return affected > 0;
    }
}

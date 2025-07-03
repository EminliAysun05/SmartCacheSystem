using AutoMapper;
using SmartCacheProject.Domain.Dtos.Category;
using SmartCacheProject.Domain.Entities;

namespace SmartCacheProject.Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponseDto>().ReverseMap();
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap();
    }
}

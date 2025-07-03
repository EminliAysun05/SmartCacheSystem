using AutoMapper;
using SmartCacheProject.Domain.Dtos.Story;
using SmartCacheProject.Domain.Entities;

namespace SmartCacheProject.Application.Mappings
{
    public class StoryProfile : Profile
    {
        public StoryProfile()
        {
            CreateMap<Story,StoryResponseDto>().ReverseMap();
            CreateMap<Story, StoryCreateDto>().ReverseMap();
            CreateMap<Story, StoryUpdateDto>().ReverseMap();
        }
    }
}

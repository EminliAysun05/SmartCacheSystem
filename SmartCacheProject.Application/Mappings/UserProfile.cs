using AutoMapper;
using SmartCacheProject.Domain.Dtos.UserProfile;

namespace SmartCacheProject.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserProfile, UserProfileResponseDto>().ReverseMap();
        CreateMap<UserProfile, UserProfileCreateDto>().ReverseMap();
        CreateMap<UserProfile, UserProfileUpdateDto>().ReverseMap();
    }
}


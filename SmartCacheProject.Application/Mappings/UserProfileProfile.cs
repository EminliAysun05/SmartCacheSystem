using AutoMapper;
using SmartCacheProject.Domain.Dtos.UserProfile;

namespace SmartCacheProject.Application.Mappings;

public class UserProfileProfile : Profile
{
    public UserProfileProfile()
    {
        CreateMap<UserProfileProfile, UserProfileResponseDto>().ReverseMap();
        CreateMap<UserProfileProfile, UserProfileCreateDto>().ReverseMap();
        CreateMap<UserProfileProfile, UserProfileUpdateDto>().ReverseMap();
    }
}


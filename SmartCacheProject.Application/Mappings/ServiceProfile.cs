using AutoMapper;
using SmartCacheProject.Domain.Dtos.Service;
using SmartCacheProject.Domain.Entities;

namespace SmartCacheProject.Application.Mappings;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, ServiceResponseDto>().ReverseMap();
        CreateMap<Service, ServiceCreateDto>().ReverseMap();
        CreateMap<Service, ServiceUpdateDto>().ReverseMap();
    }
}

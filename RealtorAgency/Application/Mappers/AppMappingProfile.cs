using Application.Dtos.AnalyticsDtos;
using Application.Dtos.RepositoryDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

/// <summary>
/// AutoMapper profile for mapping between domain entities and their DTOs.
/// </summary>
public class AppMappingProfile : Profile
{
    /// <summary>
    /// Initializes the mappings between DTOs and domain entities.
    /// </summary>
    public AppMappingProfile()
    {
        // Repository DTOs mappings
        CreateMap<ClientEditDto, Client>().ReverseMap();
        CreateMap<ClientGetDto, Client>().ReverseMap();

        CreateMap<PropertyEditDto, Property>().ReverseMap();
        CreateMap<PropertyGetDto, Property>().ReverseMap();

        CreateMap<RequestEditDto, Request>().ReverseMap();
        CreateMap<RequestGetDto, Request>().ReverseMap();

        // Analytics DTOs mappings
        CreateMap<ClientDto, Client>().ReverseMap();
        CreateMap<ClientWithRequestCountDto, Client>().ReverseMap();
        CreateMap<ClientWithAmountDto, Client>().ReverseMap();
        CreateMap<PropertyWithRequestCountDto, Property>().ReverseMap();
    }
}

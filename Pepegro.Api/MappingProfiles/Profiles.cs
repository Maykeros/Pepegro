namespace Pepegro.Api.MappingProfiles;

using AutoMapper;
using Domain.DTO_s;
using Domain.DTO_s.MainEntities;
using Domain.Entities.Authorization;
using Domain.Entities.MainEntities;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<RegisterDTO, User>().ReverseMap();
        CreateMap<LoginDTO, User>().ReverseMap();
        CreateMap<GetOrderDto, Order>().ReverseMap();
        CreateMap<CreateProductDTO, Product>().ReverseMap();
        CreateMap<UpdateProductDTO, Product>().ReverseMap();
        CreateMap<GetProductDto, Product>().ReverseMap();
    }
}
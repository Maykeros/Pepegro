namespace Pepegro.Api.MappingProfiles;

using AutoMapper;
using Domain.DTO_s;
using Domain.Entities.Authorization;

public class AuthenticationProfiles : Profile
{
    public AuthenticationProfiles()
    {
        CreateMap<RegisterDTO, User>().ReverseMap();
        CreateMap<LoginDTO, User>().ReverseMap();
    }
}
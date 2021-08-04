using AutoMapper;
using CBP.Usuarios.API.DTO;
using CBP.Usuarios.API.Models;

namespace CBP.Usuarios.API.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMap<UsuarioRegistro, UsuarioDTO>()
        .ForMember(d => d.Email, m => m.MapFrom(o => o.Email))
        .ForMember(d => d.Funcao, m => m.MapFrom(o => o.Funcao))
        .ReverseMap();
    }

  }

}

using AutoMapper;
using CBP.WebApp.MVC.DTO;
using CBP.WebApp.MVC.Models;

namespace CBP.WebApp.MVC.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMap<ResponsavelViewModel, ResponsavelDTO> ()
        .ForMember(d => d.Funcao, o => o.MapFrom(s => s.Funcao.ToString()))
        .ReverseMap();

      //CreateMap<UsuarioRegistro, UsuarioDTO>()
      //  .ForMember(d => d.Funcao, o => o.MapFrom(s => s.Funcao.ToString()))
      //  .ReverseMap();
    }

  }

}

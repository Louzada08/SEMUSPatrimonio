using AutoMapper;
using CBP.WebApp.MVC.DTO;
using CBP.WebApp.MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace CBP.WebApp.MVC.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMap<ResponsavelViewModel, ResponsavelDTO> ()
        .ForMember(d => d.Funcao, o => o.MapFrom(s => s.Funcao.ToString()))
        .ReverseMap();

      CreateMap<IdentityUser, UsuarioDTO>()
        .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
        .ForMember(d => d.Nome, o => o.MapFrom(s => s.UserName))
        .ForMember(d => d.Senha, o => o.MapFrom(s => s.PasswordHash))
        .ForMember(d => d.SenhaConfirmacao, o => o.MapFrom(s => s.PasswordHash))
        .ReverseMap();
    }

  }

}

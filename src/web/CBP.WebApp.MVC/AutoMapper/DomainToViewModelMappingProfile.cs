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
        .ForMember(d => d.Endereco, o => o.MapFrom(s => s.Endereco))
        .ReverseMap();

      //CreateMap<EnderecoViewModel, EnderecoDTO>()
      //  .ReverseMap();

      CreateMap<UsuarioRegistro, UsuarioDTO>()
        .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
        .ForMember(d => d.Nome, o => o.MapFrom(s => s.Nome))
        .ForMember(d => d.Funcao, o => o.MapFrom(s => s.Funcao.ToString()))
        .ForMember(d => d.Senha, o => o.MapFrom(s => s.Senha))
        .ForMember(d => d.SenhaConfirmacao, o => o.MapFrom(s => s.SenhaConfirmacao))
        .ReverseMap();

      CreateMap<RoleRegistroViewModel, IdentityRole>()
        .ForMember(d => d.Name, o => o.MapFrom(s => s.FuncaoNome))
        .ReverseMap();
    }

  }

}

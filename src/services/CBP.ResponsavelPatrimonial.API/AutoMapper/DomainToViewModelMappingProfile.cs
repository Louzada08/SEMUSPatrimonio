using AutoMapper;
using CBP.ResponsavelPatrimonial.API.DTO;
using CBP.ResponsavelPatrimonial.API.Models;

namespace CBP.ResponsavelPatrimonial.API.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMap<Responsavel, ResponsavelDTO>()
        .ForMember(d => d.Email, m => m.MapFrom(o => o.Email.Endereco))
        .ForMember(d => d.Funcao, m => m.MapFrom(o => o.Funcao))
        .ForMember(d => d.EnderecoDTO, m => m.MapFrom(o => o.Endereco))
        .ReverseMap();

     // CreateMap<Endereco, EnderecoDTO>();

    }

  }

}

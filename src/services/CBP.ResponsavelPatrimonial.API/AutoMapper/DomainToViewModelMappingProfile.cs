using AutoMapper;
using CBP.ResponsavelPatrimonial.API.DTO;
using CBP.ResponsavelPatrimonial.API.Models;

namespace CBP.ResponsavelPatrimonial.API.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMap<Responsavel, UsuarioViewModel>()
        .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.Endereco))
        .ReverseMap();
    }

  }

}

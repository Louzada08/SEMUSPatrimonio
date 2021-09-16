using CBP.Core.DomainObjects;
using System;

namespace CBP.ResponsavelPatrimonial.API.Application.DTO
{
  public class ResponsavelNomeDTO
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ResponsavelNomeDTO() { }
    public ResponsavelNomeDTO(Guid id, string nome)
    {
      Id = id;
      Nome = nome;
    }
  }
}

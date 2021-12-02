using CBP.WebAPI.Core.Usuario;
using System;

namespace CBP.ResponsavelPatrimonial.API.DTO
{
  public class ResponsavelDTO
  {
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public Funcoes Funcao { get; set; }

    public bool Excluido { get; set; }

    public EnderecoDTO EnderecoDTO { get; set; }
  }
}
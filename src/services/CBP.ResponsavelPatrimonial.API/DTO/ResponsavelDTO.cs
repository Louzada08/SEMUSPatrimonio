using CBP.WebAPI.Core.Usuario;
using CBP.Core.DomainObjects;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

  public class EnderecoDTO
  {
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cep { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public Guid ResponsavelId { get; set; }
  }

}
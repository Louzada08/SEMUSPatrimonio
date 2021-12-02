using System;
using CBP.Core.DomainObjects;

namespace CBP.WebApp.MVC.DTO
{
  public class ResponsavelDTO
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Funcao { get; set; }
    public Email Email { get; set; }
    public bool Excluido { get; set; }
    public EnderecoDTO Endereco { get; set; }
    public Guid UnidadeId { get; set; }
    public UnidadeDTO UnidadeDTO { get; set; }
  }
}
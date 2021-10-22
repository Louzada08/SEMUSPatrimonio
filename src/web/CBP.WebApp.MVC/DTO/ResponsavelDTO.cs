using System;

namespace CBP.WebApp.MVC.DTO
{
  public class ResponsavelDTO
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Funcao { get; set; }
    public bool Excluido { get; set; }
    public EnderecoDTO Endereco { get; set; }
  }
}
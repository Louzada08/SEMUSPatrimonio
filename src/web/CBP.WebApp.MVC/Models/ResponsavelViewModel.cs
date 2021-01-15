using System;

namespace CBP.WebApp.MVC.Models
{
  public class ResponsavelViewModel
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Funcao { get; set; }
    public Email Email { get; set; }
    public bool Excluido { get; set; }
  }

  public class Email
  {
    public string Endereco { get; set; }
  }
  public class EnderecoViewModel
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
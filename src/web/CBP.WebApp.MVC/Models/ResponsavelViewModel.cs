using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CBP.WebApp.MVC.Models
{
  public class ResponsavelViewModel
  {
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Nome Completo")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Função")]
    public string Funcao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    [DisplayName("E-mail")]
    public string Email { get; set; }
    public bool Excluido { get; set; }
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
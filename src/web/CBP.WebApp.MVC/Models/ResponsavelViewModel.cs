using CBP.WebAPI.Core.Usuario;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CBP.WebApp.MVC.Extensions;

namespace CBP.WebApp.MVC.Models
{
  public class ResponsavelViewModel
  {
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Nome Completo")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    //[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    [Email]
    public string Email { get; set; }

    public Funcoes Funcao { get; set; }

    public bool Excluido { get; set; }

    public EnderecoViewModel Endereco { get; set; }
  }
}
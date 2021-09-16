using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CBP.WebApp.MVC.Models
{
  public class ResetSenhaViewModel
  {
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    [DisplayName("E-mail")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    [DisplayName("Nova Senha")]
    public string NewPassword { get; set; }

  }

}
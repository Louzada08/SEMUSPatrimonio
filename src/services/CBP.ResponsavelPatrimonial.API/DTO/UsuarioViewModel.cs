using CBP.WebAPI.Core.Usuario;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CBP.ResponsavelPatrimonial.API.DTO
{
  public class UsuarioViewModel
  {
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Nome Completo")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Email { get; set; }

    public Funcoes Funcao { get; set; }

    public bool Excluido { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Senha { get; set; }

    [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
    public string SenhaConfirmacao { get; set; }
  }

}
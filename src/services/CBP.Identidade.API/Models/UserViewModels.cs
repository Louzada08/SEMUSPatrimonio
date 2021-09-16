using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CBP.Identidade.API.Models
{
  public class UsuarioRegistro
  {
    public string Nome { get; set; }

    public string Email { get; set; }

    public string Funcao { get; set; }

    public bool Excluido { get; set; }

    public string Senha { get; set; }

    public string SenhaConfirmacao { get; set; }
  }

  public class ResetSenhaViewModel
  {
    public string UserName { get; set; }
    public string NewPassword { get; set; }
  }

  public class UsuarioLogin
  {
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Senha { get; set; }
  }

  public class UsuarioRespostaLogin
  {
    public string AccessToken { get; set; }
    public Guid RefreshToken { get; set; }
    public double ExpiresIn { get; set; }
    public UsuarioToken UsuarioToken { get; set; }
  }

  public class UsuarioToken
  {
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UsuarioClaim> Claims { get; set; }
  }

  public class UsuarioClaim
  {
    public string Value { get; set; }
    public string Type { get; set; }
  }
}
using CBP.WebAPI.Core.Usuario;
using System;

namespace CBP.Usuarios.API.DTO
{
  public class UsuarioDTO
  {
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public Funcoes Funcao { get; set; }

    public string Senha { get; set; }

    public string SenhaConfirmacao { get; set; }

  }

}
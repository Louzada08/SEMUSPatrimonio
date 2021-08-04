using System;
using CBP.Core.Messages;

namespace CBP.Usuarios.API.Application.Commands
{
  public abstract class UsuarioCommand : Command
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public string Funcao { get; protected set; }
    public string Email { get; protected set; }

  }
}
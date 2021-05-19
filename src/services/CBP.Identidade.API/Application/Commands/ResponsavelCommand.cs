using System;
using CBP.Core.Messages;

namespace CBP.Identidade.API.Application.Commands
{
  public abstract class ResponsavelCommand : Command
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public string Funcao { get; protected set; }
    public string Email { get; protected set; }
    public bool Excluido { get; protected set; }

  }
}
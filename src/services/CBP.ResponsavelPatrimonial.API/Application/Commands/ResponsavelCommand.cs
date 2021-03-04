using System;
using CBP.Core.DomainObjects;
using CBP.Core.Messages;

namespace CBP.ResponsavelPatrimonial.API.Application.Commands
{
  public abstract class ResponsavelCommand : Command
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Funcao { get; set; }
    public Email Email { get; set; }
    public bool Excluido { get; set; }

  }
}
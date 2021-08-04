using System;
using CBP.Core.Messages;

namespace CBP.Usuarios.API.Application.Events
{
  public class UsuarioRemovidoEvent : Event
  {
    public Guid Id { get; set; }

    public UsuarioRemovidoEvent(Guid id)
    {
      AggregateId = id;
      Id = id;
    }
  }
}
using System;
using CBP.Core.Messages;

namespace CBP.Identidade.API.Application.Events
{
  public class ResponsavelRemovidoEvent : Event
  {
    public Guid Id { get; set; }

    public ResponsavelRemovidoEvent(Guid id)
    {
      AggregateId = id;
      Id = id;
    }
  }
}
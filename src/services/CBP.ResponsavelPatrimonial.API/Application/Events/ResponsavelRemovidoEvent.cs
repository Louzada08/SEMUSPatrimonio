using System;
using CBP.Core.Messages;

namespace CBP.ResponsavelPatrimonial.API.Application.Events
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
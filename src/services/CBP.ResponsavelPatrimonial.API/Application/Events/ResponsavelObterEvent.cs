using System;
using CBP.Core.Messages;

namespace CBP.ResponsavelPatrimonial.API.Application.Events
{
  public class ResponsavelObterEvent : Event
  {
    public Guid Id { get; set; }

    public ResponsavelObterEvent(Guid id)
    {
      AggregateId = id;
      Id = id;
    }
  }
}
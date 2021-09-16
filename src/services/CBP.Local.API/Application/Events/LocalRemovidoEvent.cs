using System;
using CBP.Core.Messages;

namespace CBP.Local.API.Application.Events
{
  public class LocalRemovidoEvent : Event
  {
    public Guid Id { get; set; }

    public LocalRemovidoEvent(Guid id)
    {
      AggregateId = id;
      Id = id;
    }
  }
}
using System;
using CBP.Core.Messages;

namespace CBP.Local.API.Application.Events
{
  public class LocalObterEvent : Event
  {
    public Guid Id { get; set; }

    public LocalObterEvent(Guid id)
    {
      AggregateId = id;
      Id = id;
    }
  }
}
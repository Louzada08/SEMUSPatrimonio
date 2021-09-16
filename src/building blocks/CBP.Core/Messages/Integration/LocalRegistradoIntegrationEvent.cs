using System;

namespace CBP.Core.Messages.Integration
{
  public class LocalRegistradoIntegrationEvent : IntegrationEvent
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }

    public LocalRegistradoIntegrationEvent(Guid id, string nome)
    {
      Id = id;
      Nome = nome;
    }
  }
}
using System;

namespace CBP.Core.Messages.Integration
{
  public class ResponsavelIntegrationEvent : IntegrationEvent
  {
    public Guid ResponsavelId { get; private set; }
    public string ResponsavelNome { get; private set; }

    public ResponsavelIntegrationEvent(Guid responsavelId, string responsavelNome)
    {
      ResponsavelId = responsavelId;
      ResponsavelNome = responsavelNome;
    }
  }
}
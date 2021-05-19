using System;

namespace CBP.Core.Messages.Integration
{
    public class TermoRealizadoIntegrationEvent : IntegrationEvent
    {
        public Guid ClienteId { get; private set; }

        public TermoRealizadoIntegrationEvent(Guid clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
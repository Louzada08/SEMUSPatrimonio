using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CBP.Transferencia.API.Data;
using CBP.Core.Messages.Integration;
using CBP.MessageBus;

namespace CBP.Transferencia.API.Services
{
    public class TransferenciaIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public TransferenciaIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<TermoRealizadoIntegrationEvent>("PedidoRealizado", async request =>
                await ApagarCarrinho(request));
        }

        private async Task ApagarCarrinho(TermoRealizadoIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TermoTransferenciaContext>();

            var carrinho = await context.TermoTransferencia
                .FirstOrDefaultAsync(c => c.Id == message.ClienteId);

            if (carrinho != null)
            {
                context.TermoTransferencia.Remove(carrinho);
                await context.SaveChangesAsync();
            }
        }
    }
}
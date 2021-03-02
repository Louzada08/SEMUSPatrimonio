using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CBP.Core.Mediator;
using CBP.Core.Messages.Integration;
using CBP.MessageBus;
using CBP.ResponsavelPatrimonial.API.Application.Commands;

namespace CBP.ResponsavelPatrimonial.API.Services
{
    public class RegistroResponsavelIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroResponsavelIntegrationHandler(
                            IServiceProvider serviceProvider, 
                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
                await RegistrarResponsavel(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegistrarResponsavel(UsuarioRegistradoIntegrationEvent message)
        {
            var responsavelCommand = new RegistrarResponsavelCommand(message.Id, message.Nome, message.Funcao, message.Email, message.Excluido);
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(responsavelCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
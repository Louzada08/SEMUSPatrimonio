using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CBP.Core.Mediator;
using CBP.Core.Messages.Integration;
using CBP.MessageBus;
using CBP.Usuarios.API.Application.Commands;

namespace CBP.Usuarios.API.Services
{
  public class RegistroUsuarioIntegrationHandler : BackgroundService
  {
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegistroUsuarioIntegrationHandler(
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
      var usuarioCommand = new RegistrarUsuarioCommand(message.Id, message.Nome, message.Funcao, message.Email);

      ValidationResult sucesso;

      using (var scope = _serviceProvider.CreateScope())
      {
        var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
        sucesso = await mediator.EnviarComando(usuarioCommand);
      }

      return new ResponseMessage(sucesso);
    }
  }
}
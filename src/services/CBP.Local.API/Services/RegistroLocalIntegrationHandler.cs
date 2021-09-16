using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CBP.Core.Mediator;
using CBP.Core.Messages.Integration;
using CBP.MessageBus;
using CBP.Local.API.Application.Commands;

namespace CBP.Local.API.Services
{
  public class RegistroLocalIntegrationHandler : BackgroundService
  {
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegistroLocalIntegrationHandler(
                        IServiceProvider serviceProvider,
                        IMessageBus bus)
    {
      _serviceProvider = serviceProvider;
      _bus = bus;
    }

    private void SetResponder()
    {
      _bus.RespondAsync<LocalRegistradoIntegrationEvent, ResponseMessage>(async request =>
          await RegistrarLocal(request));

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

    private async Task<ResponseMessage> RegistrarLocal(LocalRegistradoIntegrationEvent message)
    {
      var LocalCommand = new RegistrarLocalCommand(message.Id, message.Nome);

      ValidationResult sucesso;

      using (var scope = _serviceProvider.CreateScope())
      {
        var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
        sucesso = await mediator.EnviarComando(LocalCommand);
      }

      return new ResponseMessage(sucesso);
    }
  }
}
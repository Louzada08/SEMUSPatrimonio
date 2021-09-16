using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CBP.Local.API.Application.Events
{
  public class LocalEventHandler : INotificationHandler<LocalRegistradoEvent>
  {
    public Task Handle(LocalRegistradoEvent notification, CancellationToken cancellationToken)
    {
      // Enviar evento de confirmação
      return Task.CompletedTask;
    }

  }
}
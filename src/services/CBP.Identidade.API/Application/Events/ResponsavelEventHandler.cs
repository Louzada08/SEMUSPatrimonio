using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CBP.Identidade.API.Application.Events
{
  public class ResponsavelEventHandler : INotificationHandler<ResponsavelRegistradoEvent>
  {
    public Task Handle(ResponsavelRegistradoEvent notification, CancellationToken cancellationToken)
    {
      // Enviar evento de confirmação
      return Task.CompletedTask;
    }

  }
}
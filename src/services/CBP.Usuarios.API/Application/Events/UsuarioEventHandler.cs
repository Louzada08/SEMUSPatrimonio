using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CBP.Usuarios.API.Application.Events
{
  public class UsuarioEventHandler : INotificationHandler<UsuarioRegistradoEvent>
  {
    public Task Handle(UsuarioRegistradoEvent notification, CancellationToken cancellationToken)
    {
      // Enviar evento de confirmação
      return Task.CompletedTask;
    }

  }
}
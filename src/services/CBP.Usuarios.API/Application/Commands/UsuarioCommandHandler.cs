using CBP.Core.Messages;
using CBP.Usuarios.API.Application.Events;
using CBP.Usuarios.API.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CBP.Usuarios.API.Application.Commands
{
  public class UsuarioCommandHandler : CommandHandler, 
        IRequestHandler<RegistrarUsuarioCommand, ValidationResult>,
        IRequestHandler<AtualizarUsuarioCommand, ValidationResult>
  {
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioCommandHandler(IUsuarioRepository usuarioRepository)
    {
      _usuarioRepository = usuarioRepository;
    }

    public async Task<ValidationResult> Handle(RegistrarUsuarioCommand message, CancellationToken cancellationToken)
    {
      if (!message.EhValido()) return message.ValidationResult;

      var usuario = new Usuario(message.Id, message.Nome, message.Funcao, message.Email);

      var usuarioExistente = await _usuarioRepository.ObterPorEmail(usuario.Email);

      if (usuarioExistente != null)
      {
          AdicionarErro("Este Email já está em uso.");
          return ValidationResult;
      }

      _usuarioRepository.Adicionar(usuario);

      usuario.AdicionarEvento(new UsuarioRegistradoEvent(message.Id, message.Nome, message.Funcao, message.Email));

      return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AtualizarUsuarioCommand message, CancellationToken cancellationToken)
    {
      if (!message.EhValido()) return message.ValidationResult;

      var usuario = new Usuario(message.Id, message.Nome, message.Funcao, message.Email);

      var usuarioExistente = await _usuarioRepository.ObterPorEmail(usuario.Email);

      if (usuarioExistente != null && usuarioExistente.Id != usuario.Id)
      {
        if (!usuarioExistente.Equals(usuario))
        {
          AdicionarErro("Este Email já está em uso.");
          return ValidationResult;
        }
      }

      _usuarioRepository.Atualizar(usuario);

      usuario.AdicionarEvento(new UsuarioAtualizadoEvent(message.Id, message.Nome, message.Funcao, message.Email));

      return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoverUsuarioCommand message, CancellationToken cancellationToken)
    {
      if (!message.EhValido()) return message.ValidationResult;

      var responsavel = await _usuarioRepository.GetResponsavelId(message.Id);

      if (responsavel is null)
      {
          AdicionarErro("Este responsavel não existe.");
          return ValidationResult;
      }

      _usuarioRepository.Remover(responsavel);

      responsavel.AdicionarEvento(new UsuarioRemovidoEvent(message.Id));

      return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

  }
}
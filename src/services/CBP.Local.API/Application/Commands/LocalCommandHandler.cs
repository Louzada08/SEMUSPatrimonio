
using CBP.Core.Messages;
using CBP.Local.API.Application.DTO;
using CBP.Local.API.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CBP.Local.API.Application.Commands
{
  public class LocalCommandHandler : CommandHandler, 
        IRequestHandler<RegistrarLocalCommand, ValidationResult>,
        IRequestHandler<ObterLocalCommand, ValidationResult>
  {
    private readonly ILocalRepository _localRepository;

    public LocalCommandHandler(ILocalRepository LocalRepository)
    {
      _localRepository = LocalRepository;
    }

    public async Task<ValidationResult> Handle(ObterLocalCommand message, CancellationToken cancellationToken)
    {
      var LocalExistente = await _localRepository.GetLocalId(message.Id);

      if (LocalExistente == null)
      {
        AdicionarErro("Este Local não existe.");
      }

      var Local = new LocalNomeDTO(LocalExistente.Id, LocalExistente.Nome);

      if (!message.EhValido()) return message.ValidationResult;

      return ValidationResult;
    }

    public Task<ValidationResult> Handle(RegistrarLocalCommand request, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    //public async Task<ValidationResult> Handle(AtualizarLocalCommand message, CancellationToken cancellationToken)
    //{
    //  if (!message.EhValido()) return message.ValidationResult;

    //  var Local = new Local(message.Id, message.Nome, message.Funcao, message.Email, message.Excluido);

    //  var LocalExistente = await _LocalRepository.ObterPorEmail(Local.Email.Endereco);

    //  if (LocalExistente != null && LocalExistente.Id != Local.Id)
    //  {
    //    if (!LocalExistente.Equals(Local))
    //    {
    //      AdicionarErro("Este Email já está em uso.");
    //      return ValidationResult;
    //    }
    //  }

    //  _LocalRepository.Atualizar(Local);

    //  Local.AdicionarEvento(new LocalAtualizadoEvent(message.Id, message.Nome, message.Funcao, message.Email, message.Excluido));

    //  return await PersistirDados(_LocalRepository.UnitOfWork);
    //}

    //public async Task<ValidationResult> Handle(RemoverLocalCommand message, CancellationToken cancellationToken)
    //{
    //  if (!message.EhValido()) return message.ValidationResult;

    //  var Local = await _LocalRepository.GetLocalId(message.Id);

    //  if (Local is null)
    //  {
    //      AdicionarErro("Este Local não existe.");
    //      return ValidationResult;
    //  }

    //  _LocalRepository.Remover(Local);

    //  Local.AdicionarEvento(new LocalRemovidoEvent(message.Id));

    //  return await PersistirDados(_LocalRepository.UnitOfWork);
    //}

  }
}
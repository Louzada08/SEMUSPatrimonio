
using CBP.Core.Messages;
using CBP.ResponsavelPatrimonial.API.Application.Events;
using CBP.ResponsavelPatrimonial.API.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CBP.ResponsavelPatrimonial.API.Application.Commands
{
  public class ResponsavelCommandHandler : CommandHandler,
        IRequestHandler<RegistrarResponsavelCommand, ValidationResult>
  {
    private readonly IResponsavelRepository _responsavelRepository;

    public ResponsavelCommandHandler(IResponsavelRepository responsavelRepository)
    {
      _responsavelRepository = responsavelRepository;
    }

    public async Task<ValidationResult> Handle(RegistrarResponsavelCommand message, CancellationToken cancellationToken)
    {
      if (!message.EhValido()) return message.ValidationResult;

      var responsavel = new Responsavel(message.Id, message.Nome, message.Email);

      //var responsavelExistente = await _responsavelRepository.ObterPorCpf(responsavel.Cpf.Numero);

      //if (responsavelExistente != null)
      //{
      //    AdicionarErro("Este CPF já está em uso.");
      //    return ValidationResult;
      //}

      _responsavelRepository.Adicionar(responsavel);

      responsavel.AdicionarEvento(new ResponsavelRegistradoEvent(message.Id, message.Nome, message.Email));

      return await PersistirDados(_responsavelRepository.UnitOfWork);
    }
  }
}
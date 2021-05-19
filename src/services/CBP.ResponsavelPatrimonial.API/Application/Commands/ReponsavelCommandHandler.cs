
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
        IRequestHandler<RegistrarResponsavelCommand, ValidationResult>,
        IRequestHandler<AtualizarResponsavelCommand, ValidationResult>,
        IRequestHandler<AdicionarEnderecoCommand, ValidationResult>
  {
    private readonly IResponsavelRepository _responsavelRepository;

    public ResponsavelCommandHandler(IResponsavelRepository responsavelRepository)
    {
      _responsavelRepository = responsavelRepository;
    }

    public async Task<ValidationResult> Handle(RegistrarResponsavelCommand message, CancellationToken cancellationToken)
    {
      if (!message.EhValido()) return message.ValidationResult;

      var responsavel = new Responsavel(message.Id, message.Nome, message.Funcao, message.Email, message.Excluido);

      var responsavelExistente = await _responsavelRepository.ObterPorEmail(responsavel.Email.Endereco);

      if (responsavelExistente != null)
      {
          AdicionarErro("Este Email já está em uso.");
          return ValidationResult;
      }

      _responsavelRepository.Adicionar(responsavel);

      responsavel.AdicionarEvento(new ResponsavelRegistradoEvent(message.Id, message.Nome, message.Funcao, message.Email, message.Excluido));

      return await PersistirDados(_responsavelRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AtualizarResponsavelCommand message, CancellationToken cancellationToken)
    {
      if (!message.EhValido()) return message.ValidationResult;

      var responsavel = new Responsavel(message.Id, message.Nome, message.Funcao, message.Email, message.Excluido);

      var responsavelExistente = await _responsavelRepository.ObterPorEmail(responsavel.Email.Endereco);

      if (responsavelExistente != null && responsavelExistente.Id != responsavel.Id)
      {
        if (!responsavelExistente.Equals(responsavel))
        {
          AdicionarErro("Este Email já está em uso.");
          return ValidationResult;
        }
      }

      _responsavelRepository.Atualizar(responsavel);

      responsavel.AdicionarEvento(new ResponsavelAtualizadoEvent(message.Id, message.Nome, message.Funcao, message.Email, message.Excluido));

      return await PersistirDados(_responsavelRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoverResponsavelCommand message, CancellationToken cancellationToken)
    {
      if (!message.EhValido()) return message.ValidationResult;

      var responsavel = await _responsavelRepository.GetResponsavelId(message.Id);

      if (responsavel is null)
      {
          AdicionarErro("Este responsavel não existe.");
          return ValidationResult;
      }

      _responsavelRepository.Remover(responsavel);

      responsavel.AdicionarEvento(new ResponsavelRemovidoEvent(message.Id));

      return await PersistirDados(_responsavelRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AdicionarEnderecoCommand message, CancellationToken cancellationToken)
    {
      if (!message.EhValido()) return message.ValidationResult;

      var endereco = new Endereco(message.Logradouro, message.Numero, message.Complemento, message.Bairro, message.Cep, message.Cidade, message.Estado, message.ResponsavelId);
      _responsavelRepository.AdicionarEndereco(endereco);

      return await PersistirDados(_responsavelRepository.UnitOfWork);
    }


  }
}
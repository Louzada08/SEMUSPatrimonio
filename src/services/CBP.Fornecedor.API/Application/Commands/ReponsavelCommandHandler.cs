//using CBP.Core.Messages;
//using CBP.Identidade.API.Application.Events;
//using CBP.Identidade.API.Models;
//using FluentValidation.Results;
//using MediatR;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CBP.Identidade.API.Application.Commands
//{
//  public class ResponsavelCommandHandler : CommandHandler, 
//        IRequestHandler<AtualizarResponsavelCommand, ValidationResult>
//  {
//    public async Task<ValidationResult> Handle(AtualizarResponsavelCommand message, CancellationToken cancellationToken)
//    {
//      if (!message.EhValido()) return message.ValidationResult;

//      var responsavel = new ResponsavelAtualizar(message.Id, message.Nome, message.Funcao, message.Email, message.Excluido);

//      //_responsavelRepository.Atualizar(responsavel);

//      responsavel.AdicionarEvento(new ResponsavelAtualizadoEvent(message.Id, message.Nome, message.Funcao, message.Email, message.Excluido));

//      return await PersistirDados(_responsavelRepository.UnitOfWork);
//    }

//    public async Task<ValidationResult> Handle(RemoverResponsavelCommand message, CancellationToken cancellationToken)
//    {
//      if (!message.EhValido()) return message.ValidationResult;

//      //var responsavel = await _responsavelRepository.GetResponsavelId(message.Id);

//      if (responsavel is null)
//      {
//          AdicionarErro("Este responsavel não existe.");
//          return ValidationResult;
//      }

//      //_responsavelRepository.Remover(responsavel);

//      //responsavel.AdicionarEvento(new ResponsavelRemovidoEvent(message.Id));

//      //return await PersistirDados(_responsavelRepository.UnitOfWork);
//    }

//  }
//}
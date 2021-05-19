using FluentValidation;
using System;

namespace CBP.Identidade.API.Application.Commands
{
  public class AtualizarResponsavelCommand : ResponsavelCommand
  {
    public AtualizarResponsavelCommand(Guid id, string nome, string funcao, string email, bool excluido)
    {
      AggregateId = id;
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
      Excluido = excluido;
    }

    public AtualizarResponsavelCommand() { }

    public override bool EhValido()
    {
      ValidationResult = new AtualizarResponsavelValidation().Validate(this);
      return ValidationResult.IsValid;
    }

    public class AtualizarResponsavelValidation : AbstractValidator<AtualizarResponsavelCommand>
    {
      public AtualizarResponsavelValidation()
      {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("O nome do cliente não foi informado");

        RuleFor(c => c.Funcao)
            .NotEmpty()
            .WithMessage("A função informada não é válido.");

        RuleFor(c => c.Email)
            .Must(TerEmailValido)
            .WithMessage("O e-mail informado não é válido.");
      }

      protected static bool TerEmailValido(string email)
      {
        return Core.DomainObjects.Email.Validar(email);
      }
    }
  }
}
using System;
using CBP.Core.Messages;
using FluentValidation;

namespace CBP.Local.API.Application.Commands
{
  public class RegistrarLocalCommand : Command
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }

    public RegistrarLocalCommand(Guid id, string nome)
    {
      AggregateId = id;
      Id = id;
      Nome = nome;
    }

    public override bool EhValido()
    {
      ValidationResult = new RegistrarLocalValidation().Validate(this);
      return ValidationResult.IsValid;
    }

    public class RegistrarLocalValidation : AbstractValidator<RegistrarLocalCommand>
    {
      public RegistrarLocalValidation()
      {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("O nome do cliente não foi informado");

      }

      protected static bool TerEmailValido(string email)
      {
        return Core.DomainObjects.Email.Validar(email);
      }
    }
  }
}
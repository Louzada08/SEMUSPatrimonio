using System;
using CBP.Core.Messages;
using FluentValidation;

namespace CBP.ResponsavelPatrimonial.API.Application.Commands
{
  public class RemoverResponsavelCommand : Command
  {
    public Guid Id { get; private set; }
    public RemoverResponsavelCommand(Guid id)
    {
      AggregateId = id;
      Id = id;
    }

    public RemoverResponsavelCommand() { }

    public override bool EhValido()
    {
      ValidationResult = new RemoverResponsavelValidation().Validate(this);
      return ValidationResult.IsValid;
    }

    public class RemoverResponsavelValidation : AbstractValidator<RemoverResponsavelCommand>
    {
      public RemoverResponsavelValidation()
      {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
      }

      protected static bool TerEmailValido(string email)
      {
        return Core.DomainObjects.Email.Validar(email);
      }
    }
  }
}
using System;
using FluentValidation;

namespace CBP.Identidade.API.Application.Commands
{
  public class RemoverResponsavelCommand : ResponsavelCommand
  {
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
using System;
using FluentValidation;

namespace CBP.Usuarios.API.Application.Commands
{
  public class RemoverUsuarioCommand : UsuarioCommand
  {
    public RemoverUsuarioCommand(Guid id)
    {
      AggregateId = id;
      Id = id;
    }

    public RemoverUsuarioCommand() { }

    public override bool EhValido()
    {
      ValidationResult = new RemoverUsuarioValidation().Validate(this);
      return ValidationResult.IsValid;
    }

    public class RemoverUsuarioValidation : AbstractValidator<UsuarioCommand>
    {
      public RemoverUsuarioValidation()
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
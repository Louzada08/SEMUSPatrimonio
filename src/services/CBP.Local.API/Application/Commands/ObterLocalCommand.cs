using CBP.Core.Messages;
using FluentValidation;
using System;

namespace CBP.Local.API.Application.Commands
{
  public class ObterLocalCommand : Command
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ObterLocalCommand(Guid id, string nome) 
    {
      Id = id;
      Nome = nome;
    }
    public override bool EhValido()
    {
      ValidationResult = new ObtemResponsavelValidation().Validate(this);
      return ValidationResult.IsValid;
    }

    public class ObtemResponsavelValidation : AbstractValidator<ObterLocalCommand>
    {
      public ObtemResponsavelValidation()
      {
        RuleFor(c => c.Id).NotEqual(Guid.Empty)
          .WithMessage("Id do responsavel inválido");

        RuleFor(c => c.Nome).NotNull().NotEmpty()
          .WithMessage("Responsavel não existe");
      }
    }
  }
}

using FluentValidation;
using System;

namespace CBP.Usuarios.API.Application.Commands
{
  public class AtualizarUsuarioCommand : UsuarioCommand
  {
    public AtualizarUsuarioCommand(Guid id, string nome, string funcao, string email)
    {
      AggregateId = id;
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
    }

    public AtualizarUsuarioCommand() { }

    public override bool EhValido()
    {
      ValidationResult = new AtualizarUsuarioValidation().Validate(this);
      return ValidationResult.IsValid;
    }

    public class AtualizarUsuarioValidation : AbstractValidator<AtualizarUsuarioCommand>
    {
      public AtualizarUsuarioValidation()
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
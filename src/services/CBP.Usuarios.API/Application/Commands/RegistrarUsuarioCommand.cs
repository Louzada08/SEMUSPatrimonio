using System;
using FluentValidation;

namespace CBP.Usuarios.API.Application.Commands
{
  public class RegistrarUsuarioCommand : UsuarioCommand
  {
    public RegistrarUsuarioCommand(Guid id, string nome, string funcao, string email)
    {
      AggregateId = id;
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
    }

    public override bool EhValido()
    {
      ValidationResult = new RegistrarUsuarioValidation().Validate(this);
      return ValidationResult.IsValid;
    }

    public class RegistrarUsuarioValidation : AbstractValidator<UsuarioCommand>
    {
      public RegistrarUsuarioValidation()
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

      //protected static bool TerCpfValido(string cpf)
      //{
      //    return Core.DomainObjects.Cpf.Validar(cpf);
      //}

      protected static bool TerEmailValido(string email)
      {
        return Core.DomainObjects.Email.Validar(email);
      }
    }
  }
}
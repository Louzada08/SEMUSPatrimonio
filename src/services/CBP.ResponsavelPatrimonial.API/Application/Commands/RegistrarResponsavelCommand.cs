using System;
using CBP.Core.Messages;
using FluentValidation;

namespace CBP.ResponsavelPatrimonial.API.Application.Commands
{
  public class RegistrarResponsavelCommand : Command
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Funcao { get; private set; }
    public bool Excluido { get; private set; }

    public RegistrarResponsavelCommand(Guid id, string nome, string funcao, string email, bool excluido)
    {
      AggregateId = id;
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
      Excluido = excluido;
    }

    public override bool EhValido()
    {
      ValidationResult = new RegistrarResponsavelValidation().Validate(this);
      return ValidationResult.IsValid;
    }

    public class RegistrarResponsavelValidation : AbstractValidator<RegistrarResponsavelCommand>
    {
      public RegistrarResponsavelValidation()
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
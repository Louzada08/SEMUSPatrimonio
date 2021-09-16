using CBP.Core.Messages;
using FluentValidation;
using System;

namespace CBP.Local.API.Application.Commands
{  
  public class AtualizarLocalCommand : Command
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Funcao { get; private set; }
    public string Email { get; private set; }
    public bool Excluido { get; private set; }

    public AtualizarLocalCommand(Guid id, string nome, string funcao, string email, bool excluido)
    {
      AggregateId = id;
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
      Excluido = excluido;
    }

    public AtualizarLocalCommand() { }

    public override bool EhValido()
    {
      ValidationResult = new AtualizarLocalValidation().Validate(this);
      return ValidationResult.IsValid;
    }

    public class AtualizarLocalValidation : AbstractValidator<AtualizarLocalCommand>
    {
      public AtualizarLocalValidation()
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
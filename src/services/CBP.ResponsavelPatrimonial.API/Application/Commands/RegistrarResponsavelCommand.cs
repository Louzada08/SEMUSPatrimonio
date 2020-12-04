using System;
using FluentValidation;
using CBP.Core.Messages;

namespace CBP.ResponsavelPatrimonial.API.Application.Commands
{
    public class RegistrarResponsavelCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        //public string Cpf { get; private set; }

        public RegistrarResponsavelCommand(Guid id, string nome, string email)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            //Cpf = cpf;
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

                //RuleFor(c => c.Cpf)
                //    .Must(TerCpfValido)
                //    .WithMessage("O CPF informado não é válido.");

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
using System;
using CBP.Core.Messages;

namespace CBP.ResponsavelPatrimonial.API.Application.Events
{
    public class ResponsavelRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        //public string Cpf { get; private set; }

        public ResponsavelRegistradoEvent(Guid id, string nome, string email)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            //Cpf = cpf;
        }
    }
}
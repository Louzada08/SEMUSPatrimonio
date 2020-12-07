﻿using System;

namespace CBP.Core.Messages.Integration
{
    public class UsuarioRegistradoIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
       // public string Cpf { get; private set; }

        public UsuarioRegistradoIntegrationEvent(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
           // Cpf = cpf;
        }
    }
}
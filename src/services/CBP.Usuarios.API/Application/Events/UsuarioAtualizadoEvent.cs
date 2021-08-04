using System;
using CBP.Core.Messages;

namespace CBP.Usuarios.API.Application.Events
{
  public class UsuarioAtualizadoEvent : Event
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Funcao { get; private set; }
    public string Email { get; private set; }

    public UsuarioAtualizadoEvent(Guid id, string nome, string funcao, string email) 
    { 
      AggregateId = id;
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
    }
  }
}
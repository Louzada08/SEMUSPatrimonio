using System;
using CBP.Core.Messages;

namespace CBP.Local.API.Application.Events
{
  public class LocalAtualizadoEvent : Event
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Funcao { get; private set; }
    public string Email { get; private set; }

    public bool Excluido { get; private set; }

    public LocalAtualizadoEvent(Guid id, string nome, string funcao, string email, bool excluido)
    {
      AggregateId = id;
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
      Excluido = excluido;
    }
  }
}
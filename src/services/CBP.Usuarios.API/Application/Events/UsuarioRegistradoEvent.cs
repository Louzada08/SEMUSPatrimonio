using System;
using CBP.Core.Messages;

namespace CBP.Usuarios.API.Application.Events
{
  public class UsuarioRegistradoEvent : Event
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Funcao { get; private set; }
    public string Email { get; private set; }
    public bool Excluido { get; private set; }

    public UsuarioRegistradoEvent(Guid id, string nome, string funcao, string email)
    {
      AggregateId = id;
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
    }
  }
}
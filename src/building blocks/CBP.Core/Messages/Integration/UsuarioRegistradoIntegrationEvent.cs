using System;

namespace CBP.Core.Messages.Integration
{
  public class UsuarioRegistradoIntegrationEvent : IntegrationEvent
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public string Funcao { get; protected set; }
    public string Email { get; protected set; }
    public bool Excluido { get; protected set; }

    public UsuarioRegistradoIntegrationEvent(Guid id, string nome, string funcao, string email, bool excluido)
    {
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
      Excluido = excluido;
    }
  }
}
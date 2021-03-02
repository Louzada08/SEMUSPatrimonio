using System;

namespace CBP.Core.Messages.Integration
{
  public class UsuarioRegistradoIntegrationEvent : IntegrationEvent
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Funcao { get; private set; }
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
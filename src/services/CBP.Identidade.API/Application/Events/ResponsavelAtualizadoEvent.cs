using System;
using CBP.Core.Messages.Integration;

namespace CBP.Identidade.API.Application.Events
{
  public class ResponsavelAtualizadoEvent : IntegrationEvent
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Funcao { get; private set; }
    public string Email { get; private set; }
    public bool Excluido { get; private set; }

    public ResponsavelAtualizadoEvent(Guid id, string nome, string funcao, string email, bool excluido)
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
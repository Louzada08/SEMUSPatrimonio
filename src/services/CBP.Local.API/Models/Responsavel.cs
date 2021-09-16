using System;
using CBP.Core.DomainObjects;

namespace CBP.Local.API.Models
{
  public class Responsavel : Entity, IAggregateRoot
  {
    public string Nome { get; protected set; }
    public string Funcao { get; protected set; }
    public Email Email { get; protected set; }
    public bool Excluido { get; protected set; }
    public Endereco Endereco { get; protected set; }
    public Guid UnidadeId { get; protected set; }
    public Unidade Unidade { get; set; }
    
    protected Responsavel() { }

    public Responsavel(Guid id, string nome, string funcao, string email, bool excluido = false)
    {
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = new Email(email);
      Excluido = excluido;
    }

    public void TrocarEmail(string email)
    {
      Email = new Email(email);
    }

    public void AtribuirEndereco(Endereco endereco)
    {
      Endereco = endereco;
    }

  }
}
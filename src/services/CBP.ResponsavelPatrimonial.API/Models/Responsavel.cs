using System;
using CBP.Core.DomainObjects;

namespace CBP.ResponsavelPatrimonial.API.Models
{
  public class Responsavel : Entity, IAggregateRoot
  {
    public string Nome { get; private set; }
    public string Funcao { get; set; }
    public Email Email { get; private set; }
    public bool Excluido { get; private set; }
    public Endereco Endereco { get; private set; }

    protected Responsavel() { }

    public Responsavel(Guid id, string nome, string funcao, string email)
    {
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = new Email(email);
      Excluido = false;
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
using CBP.Core.DomainObjects;
using System;

namespace CBP.Usuarios.API.Models
{
  public class Usuario : Entity, IAggregateRoot
  {
    public string Nome { get; set; }
    public string Funcao { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string SenhaConfirmacao { get; set; }

    public Usuario(Guid id, string nome, string funcao, string email)
    {
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
    }

  }
}
using CBP.Core.DomainObjects;
using System;

namespace CBP.Identidade.API.Models
{
  public class ResponsavelAtualizar : Entity
  {
    public string Nome { get; set; }

    public string Funcao { get; set; }

    public string Email { get; set; }

    public bool Excluido { get; set; }

    public ResponsavelAtualizar(Guid id, string nome, string funcao, string email, bool excluido = false)
    {
      Id = id;
      Nome = nome;
      Funcao = funcao;
      Email = email;
      Excluido = excluido;
    }

  }
}
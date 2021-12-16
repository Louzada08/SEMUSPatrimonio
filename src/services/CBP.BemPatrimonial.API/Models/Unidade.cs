using System;
using System.Collections.Generic;
using CBP.Core.DomainObjects;

namespace CBP.ResponsavelPatrimonial.API.Models
{
  public class Unidade : Entity, IAggregateRoot
  {
    public string Nome { get; private set; }

    private readonly List<Responsavel> _responsavel;
    public IReadOnlyCollection<Responsavel> Responsavel => _responsavel;

    public Unidade(string nome, List<Responsavel> responsaveis)
    {
      Nome = nome;
      _responsavel = responsaveis;
    }

    public Unidade() { }

  }
}
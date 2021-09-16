using System;
using System.Collections.Generic;
using CBP.Core.DomainObjects;

namespace CBP.Local.API.Models
{
  public class Unidade : Entity, IAggregateRoot
  {
    public Unidade(string nome, List<Responsavel> responsaveis)
    {
      Nome = nome;
      _responsavel = responsaveis;
    }

    public Unidade() { }

    public string Nome { get; private set; }
    public Guid ResponsavelId { get; private set; }

    private readonly List<Responsavel> _responsavel;
    public IReadOnlyCollection<Responsavel> Responsavel => _responsavel;
  }
}
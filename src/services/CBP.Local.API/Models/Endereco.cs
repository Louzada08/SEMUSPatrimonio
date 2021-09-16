using System;
using CBP.Core.DomainObjects;

namespace CBP.Local.API.Models
{
  public class Endereco : Entity
  {
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string Complemento { get; private set; }
    public string Bairro { get; private set; }
    public string Cep { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public Guid ResponsavelId { get; private set; }

    // EF Relation
    public Responsavel Responsavel { get; protected set; }

    protected Endereco() { }

    public Endereco(string logradouro, string numero, string complemento, string bairro,
      string cep, string cidade, string estado, Guid responsavelId)
    {
      Logradouro = logradouro;
      Numero = numero;
      Complemento = complemento;
      Bairro = bairro;
      Cep = cep;
      Cidade = cidade;
      Estado = estado;
      ResponsavelId = responsavelId;
    }
  }
}
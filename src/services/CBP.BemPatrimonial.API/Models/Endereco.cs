using System;
using CBP.Core.DomainObjects;

namespace CBP.BemPatrimonial.API.Models
{
  public readonly struct Endereco 
  {
    public string Logradouro { get; }
    public string Numero { get; }
    public string Complemento { get; }
    public string Bairro { get; }
    public string Cep { get; }
    public string Cidade { get; }
    public string Estado { get; }

    public Endereco(string logradouro, string numero, string complemento,
                    string bairro, string cep, string cidade, string estado) =>
          (Logradouro, Numero, Complemento, Bairro, Cep, Cidade, Estado) = 
          (logradouro, numero, complemento, bairro, cep, cidade, estado); 

  }
}
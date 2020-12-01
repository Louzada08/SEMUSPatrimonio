using CBP.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace CBP.BemPatrimonial.API.Models
{
  public class Pessoa : Entity
  {
    public int Matricula { get; set; }
    public string PrimeiroNome { get; set; }
    public string SegundoNome { get; set; }
    public string Cargo { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public Guid LocaisId { get; set; }
    public Local Locais { get; set; }
    public IEnumerable<Patrimonio> Patrimonios { get; set; }
    //public List<Transferencia> Transferencias { get; set; }
  }

}

using CBP.Core.DomainObjects;
using System.Collections.Generic;

namespace CBP.BemPatrimonial.API.Models
{
  public class Local : Entity
  {
    public string Nome { get; set; }
    public IEnumerable<Pessoa> Pessoas { get; set; }
    public IEnumerable<Patrimonio> Patrimonios { get; set; }
  }
}

using CBP.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBP.BemPatrimonial.API.Models
{
  public interface IPatrimonioRepository : IRepository<Patrimonio>
  {
    Task<IEnumerable<Patrimonio>> ObterTodos();
    Task<Patrimonio> ObterPorId(Guid id);

    void Adicionar(Patrimonio patrimonio);
    void Atualizar(Patrimonio patrimonio);
  }
}

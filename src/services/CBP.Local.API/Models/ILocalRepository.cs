using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBP.Core.Data;

namespace CBP.Local.API.Models
{
  public interface ILocalRepository : IRepository<Unidade>
  {
    void Adicionar(Unidade unidade);
    void Atualizar(Unidade unidade);
    void Remover(Unidade unidade);
    Task<Unidade> GetLocalId(Guid id);
    Task<IEnumerable<Unidade>> ObterTodos();
  }
}
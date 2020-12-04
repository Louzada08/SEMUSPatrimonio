using System.Collections.Generic;
using System.Threading.Tasks;
using CBP.Core.Data;

namespace CBP.ResponsavelPatrimonial.API.Models
{
  public interface IResponsavelRepository : IRepository<Responsavel>
  {
    void Adicionar(Responsavel responsavel);

    Task<IEnumerable<Responsavel>> ObterTodos();
    //Task<Responsavel> ObterPorCpf(string cpf);
  }
}
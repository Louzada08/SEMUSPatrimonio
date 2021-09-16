using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBP.Core.Data;
using CBP.ResponsavelPatrimonial.API.DTO;

namespace CBP.ResponsavelPatrimonial.API.Models
{
  public interface IResponsavelRepository : IRepository<Responsavel>
  {
    void Adicionar(Responsavel responsavel);
    void Atualizar(Responsavel responsavel);
    void Remover(Responsavel responsavel);

    void AdicionarEndereco(Endereco endereco);
    Task<Responsavel> GetResponsavelId(Guid id);
    Task<IEnumerable<Responsavel>> ObterTodos();
    Task<Responsavel> ObterPorEmail(string email);
  }
}
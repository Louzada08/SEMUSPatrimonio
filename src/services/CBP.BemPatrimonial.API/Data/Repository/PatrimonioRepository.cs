using CBP.BemPatrimonial.API.Models;
using CBP.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBP.BemPatrimonial.API.Data.Repository
{
  public class PatrimonioRepository : IPatrimonioRepository
  {
    private readonly PatrimonioContext _context;

    public PatrimonioRepository(PatrimonioContext context)
    {
      _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Patrimonio>> ObterTodos()
    {
      return await _context.Patrimonios.AsNoTracking().ToListAsync();
    }

    public async Task<Patrimonio> ObterPorId(Guid id)
    {
      return await _context.Patrimonios.FindAsync(id);
    }

    public void Adicionar(Patrimonio patrimonio)
    {
      _context.Patrimonios.Add(patrimonio);
    }

    public void Atualizar(Patrimonio patrimonio)
    {
      _context.Patrimonios.Update(patrimonio);
    }

    public void Dispose()
    {
      _context?.Dispose();
    }

  }
}

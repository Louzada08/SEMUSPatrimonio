using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CBP.Core.Data;
using CBP.Local.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CBP.Local.API.Data.Repository
{
  public class LocalRepository : ILocalRepository
  {
    private readonly LocalContext _context;
    private readonly IMapper _mapper;
    public LocalRepository(LocalContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Unidade> GetLocalId(Guid id)
    {
      return await _context.Unidades.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Unidade>> ObterTodos()
    {
      return await _context.Unidades.AsNoTracking().ToListAsync();
    }

    public void Adicionar(Unidade local)
    {
      _context.Unidades.Add(local);
    }

    public void Atualizar(Unidade local)
    {
      _context.Unidades.Update(local);
      var status = _context.SaveChanges();
    }

    public void Remover(Unidade local)
    {
      _context.Unidades.Remove(local);
    }

    public void Dispose()
    {
      _context.Dispose();
    }

  }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CBP.Core.Data;
using CBP.Usuarios.API.DTO;
using CBP.Usuarios.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CBP.Usuarios.API.Data.Repository
{
  public class UsuarioRepository : IUsuarioRepository
  {
    private readonly UsuarioContext _context;
    private readonly IMapper _mapper;
    public UsuarioRepository(UsuarioContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Usuario> GetResponsavelId(Guid id)
    {
      return await _context.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<UsuarioDTO>> ObterTodos()
    {
      return _mapper.Map<IEnumerable<UsuarioDTO>>(await _context.Responsaveis.AsNoTracking().ToListAsync());
    }

    public Task<Usuario> ObterPorEmail(string email)
    {
      return _context.Responsaveis.FirstOrDefaultAsync(c => c.Email == email);
    }

    public void Adicionar(Usuario responsavel)
    {
     // _context.Usuarios.Add(responsavel);
    }

    public void Atualizar(Usuario responsavel)
    {
      //_context.Usuarios.Update(responsavel);
      var status = _context.SaveChanges();
    }

    public void Remover(Usuario responsavel)
    {
      //_context.Usuarios.Remove(responsavel);
    }

    public void Dispose()
    {
      _context.Dispose();
    }

  }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CBP.Core.Data;
using CBP.ResponsavelPatrimonial.API.DTO;
using CBP.ResponsavelPatrimonial.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CBP.ResponsavelPatrimonial.API.Data.Repository
{
  public class ResponsavelRepository : IResponsavelRepository
  {
    private readonly ResponsavelContext _context;
    private readonly IMapper _mapper;
    public ResponsavelRepository(ResponsavelContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Responsavel> GetResponsavelId(Guid id)
    {
      return await _context.Responsaveis.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<ResponsavelDTO>> ObterTodos()
    {
      return _mapper.Map<IEnumerable<ResponsavelDTO>>(await _context.Responsaveis.AsNoTracking().ToListAsync());
    }

    public Task<Responsavel> ObterPorEmail(string email)
    {
      return _context.Responsaveis.FirstOrDefaultAsync(c => c.Email.Endereco == email);
    }

    public void Adicionar(Responsavel responsavel)
    {
      _context.Responsaveis.Add(responsavel);
    }

    public void Atualizar(Responsavel responsavel)
    {
      _context.Responsaveis.Update(responsavel);
    }

    public void Remover(Responsavel responsavel)
    {
      _context.Responsaveis.Remove(responsavel);
    }

    public void AdicionarEndereco(Endereco endereco)
    {
      _context.Enderecos.Add(endereco);
    }

    public void Dispose()
    {
      _context.Dispose();
    }

  }
}
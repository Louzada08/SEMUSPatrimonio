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
      var respEnd = await _context.Responsaveis
          .Include(e => e.Endereco)
          .FirstOrDefaultAsync(e => e.Id == id);
      
      return respEnd;
    }

    public async Task<IEnumerable<Responsavel>> ObterTodos()
    {
      var responsaveis = await _context.Responsaveis.AsNoTracking().ToListAsync();
      return responsaveis;
    }

    public Task<Responsavel> ObterPorEmail(string email)
    {
      return _context.Responsaveis.FirstOrDefaultAsync(c => c.Email.Endereco == email);
    }

    public void Adicionar(Responsavel responsavel)
    {
      _context.Responsaveis.Add(responsavel);
      var status = _context.SaveChanges();
    }

    public void Atualizar(Responsavel responsavel)
    {
      _context.Responsaveis.Update(responsavel);
      var status = _context.SaveChanges();
    }

    public void Remover(Responsavel responsavel)
    {
      _context.Responsaveis.Remove(responsavel);
    }

    public async Task<Endereco> ObterEnderecoPorId(Guid id)
    {
      return await _context.Enderecos.FirstOrDefaultAsync(e => e.ResponsavelId == id);
    }

    public bool AdicionarEndereco(Endereco endereco)
    {
      _context.Enderecos.Add(endereco);
      var status = _context.SaveChanges() > 0;
      return status;
    }

    public void Adicionar(Unidade unidade)
    {
      _context.Unidades.Add(unidade);
      var status = _context.SaveChanges();
    }

    public void Atualizar(Unidade unidade)
    {
      _context.Unidades.Update(unidade);
      var status = _context.SaveChanges();
    }

    public void Dispose()
    {
      _context.Dispose();
    }

  }
}
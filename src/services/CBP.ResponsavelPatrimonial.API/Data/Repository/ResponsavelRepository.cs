﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CBP.Core.Data;
using CBP.ResponsavelPatrimonial.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CBP.ResponsavelPatrimonial.API.Data.Repository
{
    public class ResponsavelRepository : IResponsavelRepository
    {
        private readonly ResponsavelContext _context;

        public ResponsavelRepository(ResponsavelContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Responsavel>> ObterTodos()
        {
            return await _context.Responsaveis.AsNoTracking().ToListAsync();
        }

        //public Task<Responsavel> ObterPorCpf(string cpf)
        //{
        //    return _context.Responsaveis.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        //}

        public void Adicionar(Responsavel responsavel)
        {
            _context.Responsaveis.Add(responsavel);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
  }
}
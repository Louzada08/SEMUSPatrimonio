using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBP.BemPatrimonial.API.Models;
using CBP.WebAPI.Core.Controllers;
using CBP.WebAPI.Core.Identidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBP.BemPatrimonial.API.Controllers
{
  [Authorize]
  public class PatrimonioController : MainController
  {
    private readonly IPatrimonioRepository _patrimonioRepository;

    public PatrimonioController(IPatrimonioRepository patrimonioRepository)
    {
      _patrimonioRepository = patrimonioRepository;
    }

    [AllowAnonymous]
    [HttpGet("vitrine")]
    public async Task<IEnumerable<Patrimonio>> Index()
    {
      return await _patrimonioRepository.ObterTodos();
    }

    [ClaimsAuthorize("Patrimonio", "Ler")]
    [HttpGet("patrimonio/patrimonios/{id}")]
    public async Task<Patrimonio> ProdutoDetalhe(Guid id)
    {
      return await _patrimonioRepository.ObterPorId(id);
    }
  }
}
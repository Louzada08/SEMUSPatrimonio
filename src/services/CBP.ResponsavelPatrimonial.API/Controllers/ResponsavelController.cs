using CBP.Core.Mediator;
using CBP.ResponsavelPatrimonial.API.Models;
using CBP.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBP.ResponsavelPatrimonial.API.Controllers
{
  public class ResponsavelController : MainController
  {
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public ResponsavelController(IMediatorHandler mediatorHandler, IResponsavelRepository responsavelRepository)
    {
      _mediatorHandler = mediatorHandler;
      _responsavelRepository = responsavelRepository;
    }

    [HttpGet("responsaveis")]
    public async Task<IEnumerable<Responsavel>> ObterListaResponsaveis()
    {
      return await _responsavelRepository.ObterTodos();
    }
  }
}
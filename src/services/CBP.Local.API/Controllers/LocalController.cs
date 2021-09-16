using AutoMapper;
using CBP.Core.Mediator;
using CBP.Local.API.Models;
using CBP.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBP.Local.API.Controllers
{
  public class LocalController : MainController
  {
    private readonly ILocalRepository _localRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;

    public LocalController(IMediatorHandler mediator, ILocalRepository localRepository, IMapper mapper)
    {
      _localRepository = localRepository;
      _mediator = mediator;
      _mapper = mapper;
    }

    [HttpGet("unidade/{id}")]
    public async Task<IActionResult> ObterResponsavelId(Guid id)
    {
      var unidade = await _localRepository.GetLocalId(id);

      return unidade == null ? NotFound() : CustomResponse(unidade);
    }

    [HttpGet("unidades")]
    public async Task<IEnumerable<Unidade>> ObterListaResponsaveis()
    {
      return await _localRepository.ObterTodos();
    }

    [HttpPut("unidade-editar")]
    public async Task<IActionResult> EditarResponsavel(Unidade unidadeEdita)
    {
      _localRepository.Atualizar(unidadeEdita);

      return CustomResponse(unidadeEdita);
    }

  }
}
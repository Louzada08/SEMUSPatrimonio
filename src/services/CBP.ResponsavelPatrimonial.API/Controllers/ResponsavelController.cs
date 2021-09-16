using AutoMapper;
using CBP.Core.Mediator;
using CBP.ResponsavelPatrimonial.API.DTO;
using CBP.ResponsavelPatrimonial.API.Models;
using CBP.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBP.ResponsavelPatrimonial.API.Controllers
{
  public class ResponsavelController : MainController
  {
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;

    public ResponsavelController(IMediatorHandler mediator, IResponsavelRepository responsavelRepository, IMapper mapper)
    {
      _responsavelRepository = responsavelRepository;
      _mediator = mediator;
      _mapper = mapper;
    }

    [HttpGet("responsavel/{id}")]
    public async Task<IActionResult> ObterResponsavelId(Guid id)
    {
      var responsavel = _mapper.Map<ResponsavelDTO>(await _responsavelRepository.GetResponsavelId(id));

      return responsavel == null ? NotFound() : CustomResponse(responsavel);
    }
    
    [HttpGet("responsaveis")]
    public async Task<IEnumerable<ResponsavelDTO>> ObterListaResponsaveis()
    {
      var responsaveis = _mapper.Map<IEnumerable<ResponsavelDTO>>(await _responsavelRepository.ObterTodos());
      return responsaveis;
    }

    [HttpPut("responsavel-editar")]
    public async Task<IActionResult> EditarResponsavel(ResponsavelDTO responsavelEdita)
    {

      _responsavelRepository.Atualizar(_mapper.Map<Responsavel>(responsavelEdita));

      return CustomResponse(responsavelEdita);
    }

  }
}
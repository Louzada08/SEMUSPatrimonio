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

    public ResponsavelController(IMediatorHandler mediator, IResponsavelRepository responsavelRepository)
    {
      _mediator = mediator;
      _responsavelRepository = responsavelRepository;
    }

    [HttpGet("responsavel/{id}")]
    public async Task<IActionResult> ObterResponsavelId(Guid id)
    {
      var responsavel = await _responsavelRepository.GetResponsavelId(id);

      return responsavel == null ? NotFound() : CustomResponse(responsavel);
    }

    [HttpGet("responsaveis")]
    public async Task<IEnumerable<UsuarioViewModel>> ObterListaResponsaveis()
    {
      return await _responsavelRepository.ObterTodos();
    }

    //  private async Task<AtualizarResponsavelCommand> EditarResponsavel(Responsavel responsavelEdita)
    //{
    //  var responsavel = await _responsavelRepository.GetResponsavelId(responsavelEdita.Id);

    //  var responsavelAtualizado = new UsuarioRegistradoIntegrationEvent(
    //      responsavelEdita.Id, responsavelEdita.Nome, responsavelEdita.Funcao.ToString(), responsavelEdita.Email.Endereco, responsavelEdita.Excluido);

    //  try
    //  {
    //    return await _bus.RequestAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(responsavelAtualizado);
    //  }
    //  catch
    //  {
    //    throw;
    //  }
    //}

  }
}
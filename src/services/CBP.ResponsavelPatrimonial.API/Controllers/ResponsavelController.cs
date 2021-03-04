using CBP.Core.Mediator;
using CBP.Core.Messages.Integration;
using CBP.MessageBus;
using CBP.ResponsavelPatrimonial.API.Application.Commands;
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
    public async Task<IEnumerable<Responsavel>> ObterListaResponsaveis()
    {
      return await _responsavelRepository.ObterTodos();
    }

    [HttpPut("responsavel-editar/{id}")]
    public async Task<IActionResult> Atualizar(Guid id, AtualizarResponsavelCommand responsavel)
    {
      var responsavelObter = await _responsavelRepository.GetResponsavelId(id);

      if (responsavelObter == null) return NotFound();

        var responsavelAtualizado = new AtualizarResponsavelCommand(responsavel.Id, responsavel.Nome, responsavel.Funcao, 
            responsavel.Email, responsavel.Excluido);

      return CustomResponse(await _mediator.EnviarComando(responsavelAtualizado));


      //if (responsavel is null) return NotFound();


      //if (!ModelState.IsValid) return CustomResponse(ModelState);

      //var user = await _userManager.FindByIdAsync(id.ToString());

      //if (user == null)
      //{
      //  return NotFound($"Usuário '{usuarioRegistro.Nome}' não encontrado");
      //}

      //user.Id = id.ToString();
      //user.Email = usuarioRegistro.Email;
      //user.UserName = usuarioRegistro.Email;

      //var result = await _userManager.UpdateAsync(user);

      //if (result.Succeeded)
      //{
      //  //var funcao = (short)usuarioRegistro.Funcao;


      //  //if (!responsavelResult.ValidationResult.IsValid)
      //  //{
      //  //  await _userManager.DeleteAsync(user);
      //  //  return CustomResponse(responsavelResult.ValidationResult);
      //  //}

      //  //await _userManager.AddClaimAsync(user, new Claim("NivelDeAcesso", funcao.ToString("d2")));

      //  //return CustomResponse(await GerarJwt(usuarioRegistro.Email));
      //  return CustomResponse(responsavelResult);
      //}

      //foreach (var error in result.Errors)
      //{
      //  AdicionarErroProcessamento(error.Description);
      //}

      //return CustomResponse();
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
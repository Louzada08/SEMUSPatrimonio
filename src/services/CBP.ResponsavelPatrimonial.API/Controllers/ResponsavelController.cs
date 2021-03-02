using CBP.Core.Mediator;
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

    public ResponsavelController(IMediatorHandler mediatorHandler, IResponsavelRepository responsavelRepository)
    {
      _mediator = mediatorHandler;
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

    [HttpPost("responsavel-editar")]
    public async Task<IActionResult> Atualizar(AtualizarResponsavelCommand responsavelCommand)
    {
      //var responsavel = await _responsavelRepository.GetResponsavelId(id);

      //if(responsavel == null) return NotFound();

      return CustomResponse(await _mediator.EnviarComando(responsavelCommand));


      //if (responsavel is null) return NotFound();

      //responsavelViewModel.Id = responsavelViewModel.Usuario.Id;
      //responsavelViewModel.Nome = responsavelViewModel.Usuario.Nome;
      //responsavelViewModel.Email = responsavel.Email;
      //responsavelViewModel.Excluido = responsavel.Excluido;
      //responsavelViewModel.Funcao = responsavel.Funcao;

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

      //  var responsavelResult = await EditarResponsavel(usuarioRegistro);

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

  }
}
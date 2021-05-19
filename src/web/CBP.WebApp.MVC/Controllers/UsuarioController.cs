using AutoMapper;
using CBP.WebAPI.Core.Identidade;
using CBP.WebApp.MVC.Controllers;
using CBP.WebApp.MVC.DTO;
using CBP.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBP.WebApp.MVC.Services
{
  [Authorize]
  public class UsuarioController : MainController
  {
    private readonly IAutenticacaoService _autenticacaoService;
    private readonly IResponsavelService _responsavelService;
    private readonly IMapper _mapper;

    public UsuarioController(IAutenticacaoService autenticacaoService, IResponsavelService responsavelService, IMapper mapper)
    {
      _autenticacaoService = autenticacaoService;
      _responsavelService = responsavelService;
      _mapper = mapper;
    }

    [ClaimsAuthorize("NivelDeAcesso", "Administrador")]
    [HttpGet]
    [Route("usuarios")]
    public async Task<IActionResult> Index()
    {
      var usuarios = await _responsavelService.ObterTodosUsuarios();
      return View(usuarios);
    }

    [HttpGet]
    [Route("responsavel/{id}")]
    public async Task<IActionResult> Detalhe(Guid id)
    {
      var responsavel = await _responsavelService.ObterResponsavelPorId(id);

      return View(responsavel);
    }

    [HttpPost]
    [Route("nova-conta")]
    public async Task<IActionResult> Registro(UsuarioViewModel usuarioRegistro)
    {
      if (!ModelState.IsValid) return View(usuarioRegistro);

      var resposta = await _autenticacaoService.Registro(usuarioRegistro);

      if (ResponsePossuiErros(resposta.ResponseResult)) TempData["Erros"] =
          ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

      return RedirectToAction(actionName: "Index", controllerName: "Usuario");

      //return LocalRedirect("usuarios");
    }

    [HttpPost]
    public async Task<IActionResult> Edicao(UsuarioViewModel usuarioViewModel)
    {
      var resposta = await _autenticacaoService.Atualizacao(usuarioViewModel);

      if (ResponsePossuiErros(resposta.ResponseResult))
      {
        TempData["Erros"] =
          ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
      }

      return RedirectToAction("Detalhe", "Usuario", usuarioViewModel.Id);
    }

  }
}

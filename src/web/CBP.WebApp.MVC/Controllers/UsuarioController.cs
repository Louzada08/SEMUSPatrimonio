using CBP.WebAPI.Core.Identidade;
using CBP.WebAPI.Core.Usuario;
using CBP.WebApp.MVC.Controllers;
using CBP.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CBP.WebApp.MVC.Services
{
  [Authorize]
  public class UsuarioController : MainController
  {
    private readonly IAutenticacaoService _autenticacaoService;
    private readonly IResponsavelService _responsavelService;

    public UsuarioController(IAutenticacaoService autenticacaoService, IResponsavelService responsavelService)
    {
      _autenticacaoService = autenticacaoService;
      _responsavelService = responsavelService;
    }

    [ClaimsAuthorize("NivelDeAcesso", "Administrador")]
    [HttpGet]
    [Route("usuarios")]
    public async Task<IActionResult> Index()
    {
      return View(await _responsavelService.ObterTodosUsuarios());
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

      //if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioRegistro);

      if (ResponsePossuiErros(resposta.ResponseResult)) TempData["Erros"] =
          ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

      return RedirectToAction(actionName: "Index", controllerName: "Usuario");

      //return LocalRedirect("usuarios");
    }

  }
}

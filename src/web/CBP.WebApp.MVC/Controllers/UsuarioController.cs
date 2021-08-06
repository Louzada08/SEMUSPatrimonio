using AutoMapper;
using CBP.WebAPI.Core.Identidade;
using CBP.WebApp.MVC.Controllers;
using CBP.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CBP.WebApp.MVC.Services
{
  [Authorize]
  public class UsuarioController : MainController
  {
    //private readonly IAutenticacaoService _autenticacaoService;
    //private readonly IResponsavelService _responsavelService;
    private readonly IUsuarioService _usuarioService;
    private readonly IMapper _mapper;

    public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
    {
      //_autenticacaoService = autenticacaoService;
      //_responsavelService = responsavelService;
      _usuarioService = usuarioService;
      _mapper = mapper;
    }

    [ClaimsAuthorize("NivelDeAcesso", "Responsavel")]
    [HttpGet]
    [Route("usuarios")]
    public async Task<ActionResult> Index()
    {
      var usuarios = await _usuarioService.ObterTodos();
      return View(usuarios);
    }

    [ClaimsAuthorize("NivelDeAcesso", "Responsavel")]
    [HttpGet]
    [Route("roles")]
    public async Task<ActionResult> RoleIndex()
    {
      var roles = await _usuarioService.ObterTodosRoles();
      return View(roles);
    }

    [ClaimsAuthorize("NivelDeAcesso", "Responsavel")]
    [HttpPost]
    [Route("nova-role")]
    public async Task<ActionResult> RoleRegistro(RoleRegistroViewModel roleRegistro)
    {
      if (!ModelState.IsValid) return View(roleRegistro);

      var resposta = await _usuarioService.RoleRegistro(roleRegistro);

      if (ResponsePossuiErros(resposta.ResponseResult)) return View(roleRegistro);

      return RedirectToAction("RoleIndex", "Usuario");
    }

    //[HttpGet]
    //[Route("usuario/{id}")]
    //public async Task<IActionResult> Detalhe(Guid id)
    //{
    //  var responsavel = await _responsavelService.ObterResponsavelPorId(id);

    //  return View(responsavel);
    //}

    //[HttpPost]
    //[Route("nova-conta")]
    //public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
    //{
    //  if (!ModelState.IsValid) return View(usuarioRegistro);

    //  var resposta = await _autenticacaoService.Registro(usuarioRegistro);

    //  if (ResponsePossuiErros(resposta.ResponseResult)) TempData["Erros"] =
    //      ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

    //  return RedirectToAction(actionName: "Index", controllerName: "Usuario");

    //  //return LocalRedirect("usuarios");
    //}

  }
}

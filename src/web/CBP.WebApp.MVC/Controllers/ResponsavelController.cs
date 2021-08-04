using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CBP.WebApp.MVC.Models;
using CBP.WebApp.MVC.Services;
using CBP.WebAPI.Core.Identidade;
using AutoMapper;
using System;

namespace CBP.WebApp.MVC.Controllers
{
  [Authorize]
  public class ResponsavelController : MainController
  {
    private readonly IResponsavelService _responsavelService;
    private readonly IMapper _mapper;

    public ResponsavelController(IResponsavelService responsavelService, IMapper mapper)
    {
      _responsavelService = responsavelService;
      _mapper = mapper;
    }

    [ClaimsAuthorize("NivelDeAcesso", "Responsavel")]
    [HttpGet]
    [Route("home")]
    public async Task<IActionResult> Index()
    {
      var responsaveis = await _responsavelService.ObterTodosResponsaveis();
      return View(responsaveis);
    }

    [HttpGet]
    [Route("responsavel/{id}")]
    public async Task<IActionResult> Detalhe(Guid id)
    {
      var responsavel = await _responsavelService.ObterResponsavelPorId(id);

      return View(responsavel);
    }

    //[ClaimsAuthorize("NivelDeAcesso", "Responsavel")]
    [HttpPost]
    public async Task<IActionResult> Edicao(ResponsavelViewModel responsavelViewModel)
    {
      var responsavel = await _responsavelService.ObterResponsavelPorId(responsavelViewModel.Id);

      if (responsavel == null) return NotFound();

      //var resposta = await _responsavelService.Atualizacao(_mapper.Map<ResponsavelRegistro>(usuarioViewModel));
      var resposta = await _responsavelService.Atualizacao(responsavelViewModel);

      if (ResponsePossuiErros(resposta))
      {
        TempData["Erros"] =
          ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
      }

      return RedirectToAction("Detalhe", new { id = responsavelViewModel.Id });
    }

    [HttpPost]
    public async Task<IActionResult> NovoEndereco(EnderecoViewModel endereco)
    {
      var response = await _responsavelService.AdicionarEndereco(endereco);

      if (ResponsePossuiErros(response)) TempData["Erros"] =
          ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

      return RedirectToAction("Usuario", "Detalhe");
    }
  }
}
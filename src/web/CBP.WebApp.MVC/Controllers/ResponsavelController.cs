using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CBP.WebApp.MVC.Models;
using CBP.WebApp.MVC.Services;

namespace CBP.WebApp.MVC.Controllers
{
  [Authorize]
  public class ResponsavelController : MainController
  {
    private readonly IResponsavelService _responsavelService;

    public ResponsavelController(IResponsavelService responsavelService)
    {
      _responsavelService = responsavelService;
    }

    [HttpPost]
    public async Task<IActionResult> Editar(ResponsavelViewModel responsavelViewModel)
    {
      var resposta = await _responsavelService.Atualizacao(responsavelViewModel);

      if (ResponsePossuiErros(resposta))
      {
        TempData["Erros"] =
          ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
      }

      return RedirectToAction("Detalhe","Usuario", responsavelViewModel.Id);
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
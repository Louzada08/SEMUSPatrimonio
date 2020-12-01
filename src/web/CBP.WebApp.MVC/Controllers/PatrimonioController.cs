using CBP.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CBP.WebApp.MVC.Controllers
{
  public class PatrimonioController : MainController
  {
    private readonly IPatrimonioService _patrimonioService;
    public PatrimonioController(IPatrimonioService patrimonioService)
    {
      _patrimonioService = patrimonioService;
    }

    [HttpGet]
    [Route("")]
    [Route("vitrine")]
    public async Task<IActionResult> Index()
    {
      var patrimonios = await _patrimonioService.ObterTodos();

      return View(patrimonios);
    }

    [HttpGet]
    [Route("patrimonio-detalhe/{id}")]
    public async Task<IActionResult> PatrimonioDetalhe(Guid id)
    {
      var patrimonio = await _patrimonioService.ObterPorId(id);

      return View(patrimonio);
    }
  }
}

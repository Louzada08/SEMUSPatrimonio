using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CBP.ResponsavelPatrimonial.API.Application.Commands;
using CBP.Core.Mediator;
using CBP.WebAPI.Core.Controllers;

namespace CBP.ResponsavelPatrimonial.API.Controllers
{
  public class ResponsavelController : MainController
  {
    private readonly IMediatorHandler _mediatorHandler;

    public ResponsavelController(IMediatorHandler mediatorHandler)
    {
      _mediatorHandler = mediatorHandler;
    }

    [HttpGet("responsavel")]
    public async Task<IActionResult> Index()
    {
      var resultado = await _mediatorHandler.EnviarComando(
          new RegistrarResponsavelCommand(Guid.NewGuid(), "Valdivino Patrimonio Silva", "valdivino@gmail.com"));

      if (resultado.IsValid) CustomResponse(resultado);

      return CustomResponse(resultado);
    }
  }
}
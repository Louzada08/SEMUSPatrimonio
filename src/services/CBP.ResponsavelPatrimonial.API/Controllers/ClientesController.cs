using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CBP.ResponsavelPatrimonial.API.Application.Commands;
using CBP.Core.Mediator;
using CBP.WebAPI.Core.Controllers;

namespace NSE.Clientes.API.Controllers
{
    public class ResponsavelController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ResponsavelController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            var resultado = await _mediatorHandler.EnviarComando(
                new RegistrarResponsavelCommand(Guid.NewGuid(), "Eduardo", "edu@edu.com"));

            return CustomResponse(resultado);
        }
    }
}
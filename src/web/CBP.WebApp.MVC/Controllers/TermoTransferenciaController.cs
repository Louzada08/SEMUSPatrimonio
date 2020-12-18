using System;
using System.Threading.Tasks;
using CBP.WebApp.MVC.Models;
using CBP.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBP.WebApp.MVC.Controllers
{
  [Authorize]
  public class TermoTransferenciaController : MainController
  {
    private readonly ITermoTransferenciaService _termoTransferenciaService;
    private readonly IPatrimonioService _patrimonioService;

    public TermoTransferenciaController(ITermoTransferenciaService termoTransferenciaService,
                              IPatrimonioService patrimonioService)
    {
      _termoTransferenciaService = termoTransferenciaService;
      _patrimonioService = patrimonioService;
    }

    [Route("termotransferencia")]
    public async Task<IActionResult> Index()
    {
      return View(await _termoTransferenciaService.ObterTermoTransferencia());
    }

    [HttpPost]
    [Route("termotransferencia/adicionar-item")]
    public async Task<IActionResult> AdicionarItemTermoTransferencia(ItemPatrimonioViewModel itemPatrimonio)
    {
      var patrimonio = await _patrimonioService.ObterPorId(itemPatrimonio.PatrimonioId);

      ValidarItemTermoTransferencia(patrimonio, itemPatrimonio.Quantidade);
      if (!OperacaoValida()) return View("Index", await _termoTransferenciaService.ObterTermoTransferencia());

      itemPatrimonio.Descricao = patrimonio.Descricao;
      itemPatrimonio.Valor = patrimonio.ValorBem;
      itemPatrimonio.NumeroPatrimonio = patrimonio.CodigoPatrimonio.ToString();
      //itemPatrimonio.NumeroPatrimonioCP = patrimonio.CodigoPatrimonioCP;

      var resposta = await _termoTransferenciaService.AdicionarItemTermoTransferencia(itemPatrimonio);

      if (ResponsePossuiErros(resposta)) return View("Index", await _termoTransferenciaService.ObterTermoTransferencia());

      return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("termotransferencia/atualizar-item")]
    public async Task<IActionResult> AtualizarItemCarrinho(Guid patrimonioId, int quantidade)
    {
      var patrimonio = await _patrimonioService.ObterPorId(patrimonioId);

      ValidarItemTermoTransferencia(patrimonio, quantidade);
      if (!OperacaoValida()) return View("Index", await _termoTransferenciaService.ObterTermoTransferencia());

      var itemPatrimonio = new ItemPatrimonioViewModel { PatrimonioId = patrimonioId, Quantidade = quantidade };
      var resposta = await _termoTransferenciaService.AtualizarItemTermoTransferencia(patrimonioId, itemPatrimonio);

      if (ResponsePossuiErros(resposta)) return View("Index", await _termoTransferenciaService.ObterTermoTransferencia());

      return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("termotransferencia/remover-item")]
    public async Task<IActionResult> RemoverItemCarrinho(Guid patrimonioId)
    {
      var patrimonio = await _patrimonioService.ObterPorId(patrimonioId);

      if (patrimonio == null)
      {
        AdicionarErroValidacao("Patrimonio inexistente!");
        return View("Index", await _termoTransferenciaService.ObterTermoTransferencia());
      }

      var resposta = await _termoTransferenciaService.RemoverItemTermoTransferencia(patrimonioId);

      if (ResponsePossuiErros(resposta)) return View("Index", await _termoTransferenciaService.ObterTermoTransferencia());

      return RedirectToAction("Index");
    }

    private void ValidarItemTermoTransferencia(PatrimonioViewModel patrimonio, int quantidade)
    {
      if (patrimonio == null) AdicionarErroValidacao("Patrimonio inexistente!");
      if (quantidade < 1) AdicionarErroValidacao($"Escolha ao menos uma unidade do patrimonio {patrimonio.Descricao}");
      if (quantidade > patrimonio.QuantidadeEstoque) AdicionarErroValidacao($"O patrimonio {patrimonio.Descricao} possui {patrimonio.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
    }
  }
}
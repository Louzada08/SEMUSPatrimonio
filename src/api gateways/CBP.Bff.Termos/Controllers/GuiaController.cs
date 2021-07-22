using CBP.Bff.Termos.Models;
using CBP.Bff.Termos.Services;
using CBP.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CBP.Bff.Termos.Controllers
{
  [Authorize]
  public class GuiaController : MainController
  {
    private readonly IGuiaService _guiaService;
    private readonly IPatrimonioService _patrimonioService;

    public GuiaController(IGuiaService guiaService, IPatrimonioService patrimonioService)
    {
      _guiaService = guiaService;
      _patrimonioService = patrimonioService;
    }

    [HttpGet]
    [Route("guia/transferencia")]
    public async Task<IActionResult> Index()
    {
      return CustomResponse(await _guiaService.ObterGuia());
    }

    [HttpGet]
    [Route("guia/transferencia-quantidade")]
    public async Task<int> ObterQuantidadeGuia()
    {
      var quantidade = await _guiaService.ObterGuia();
      return quantidade?.Itens.Sum(i => i.QuantidadeEstoque) ?? 0;
    }

    [HttpPost]
    [Route("guia/transferencia/items")]
    public async Task<IActionResult> AdicionarItemGuia(ItemGuiaDTO itemPatrimonio)
    {
      var patrimonio = await _patrimonioService.ObterPorId(itemPatrimonio.PatrimonioId);

      await ValidarItemGuia(patrimonio, itemPatrimonio.QuantidadeEstoque, true);
      if (!OperacaoValida()) return CustomResponse();

      itemPatrimonio.Descricao = patrimonio.Descricao;
      itemPatrimonio.ValorBem = patrimonio.ValorBem;
      itemPatrimonio.DataTransferencia = patrimonio.DataTransferencia;

      var resposta = await _guiaService.AdicionarItemGuia(itemPatrimonio);

      return CustomResponse(resposta);
    }

    [HttpPut]
    [Route("guia/transferencia/items/{patrimonioId}")]
    public async Task<IActionResult> AtualizarItemGuia(Guid patrimonioId, ItemGuiaDTO itemPatrimonio)
    {
      var patrimonio = await _patrimonioService.ObterPorId(patrimonioId);

      await ValidarItemGuia(patrimonio, itemPatrimonio.QuantidadeEstoque);
      if (!OperacaoValida()) return CustomResponse();

      var resposta = await _guiaService.AtualizarItemGuia(patrimonioId, itemPatrimonio);

      return CustomResponse(resposta);
    }

    [HttpDelete]
    [Route("guia/transferencia/items/{patrimonioId}")]
    public async Task<IActionResult> RemoverItemGuia(Guid patrimonioId)
    {
      var patrimonio = await _patrimonioService.ObterPorId(patrimonioId);

      if (patrimonio == null)
      {
        AdicionarErroProcessamento("Patrimonio inexistente!");
        return CustomResponse();
      }

      var resposta = await _guiaService.RemoverItemGuia(patrimonioId);

      return CustomResponse(resposta);
    }

    private async Task ValidarItemGuia(ItemPatrimonioDTO patrimonio, int quantidade, bool adicionarPatrimonio = false)
    {
      if (patrimonio == null) AdicionarErroProcessamento("Patrimonio inexistente!");
      if (quantidade < 1) AdicionarErroProcessamento($"Escolha ao menos uma unidade do patrimonio {patrimonio.Descricao}");

      var guia = await _guiaService.ObterGuia();
      var itemGuia = guia.Itens.FirstOrDefault(p => p.PatrimonioId == patrimonio.Id);

      if (itemGuia != null && adicionarPatrimonio && itemGuia.QuantidadeEstoque + quantidade > patrimonio.QuantidadeEstoque)
      {
        AdicionarErroProcessamento($"O produto {patrimonio.Descricao} possui {patrimonio.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
        return;
      }

      if (quantidade > patrimonio.QuantidadeEstoque) AdicionarErroProcessamento($"O produto {patrimonio.Descricao} possui {patrimonio.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
    }
  }
}

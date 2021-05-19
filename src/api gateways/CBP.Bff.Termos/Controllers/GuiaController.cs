using CBP.Bff.Termos.Models;
using CBP.Bff.Termos.Services;
using CBP.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CBP.Bff.Termos.Controllers
{
  [Authorize]
  public class GuiaController : MainController
  {
    private readonly IGuiaService _guiaService;
    private readonly IPatrimonioService _patrimonioService;
    private readonly ITermoTransferenciaService _termoService;

    public GuiaController(IGuiaService guiaService, IPatrimonioService catalogoService,
      ITermoTransferenciaService termoService)
    {
      _guiaService = guiaService;
      _patrimonioService = catalogoService;
      _termoService = termoService;
    }

    [HttpGet]
    [Route("guia/guia")]
    public async Task<IActionResult> Index()
    {
      return CustomResponse(await _guiaService.ObterGuia());
    }

    [HttpGet]
    [Route("guia/guia-quantidade")]
    public async Task<int> ObterQuantidadeCarrinho()
    {
      var quantidade = await _guiaService.ObterGuia();
      return quantidade?.Itens.Sum(i => i.QuantidadeEstoque) ?? 0;
    }

    [HttpPost]
    [Route("transferencia/guia/items")]
    public async Task<IActionResult> AdicionarItemCarrinho()
    {
      return CustomResponse();
    }

    [HttpPut]
    [Route("transferencia/guia/items/{produtoId}")]
    public async Task<IActionResult> AtualizarItemCarrinho()
    {
      return CustomResponse();
    }

    [HttpDelete]
    [Route("transferencia/guia/items/{produtoId}")]
    public async Task<IActionResult> RemoverItemCarrinho()
    {
      return CustomResponse();
    }

    private async Task ValidarItemCarrinho(ItemPatrimonioDTO produto, int quantidade, bool adicionarProduto = false)
    {
      if (produto == null) AdicionarErroProcessamento("Patrimonio inexistente!");
      if (quantidade < 1) AdicionarErroProcessamento($"Escolha ao menos uma unidade do patrimonio {produto.Descricao}");

      var guia = await _guiaService.ObterGuia();
      var itemCarrinho = guia.Itens.FirstOrDefault(p => p.PatrimonioId == produto.Id);

      if (itemCarrinho != null && adicionarProduto && itemCarrinho.QuantidadeEstoque + quantidade > produto.QuantidadeEstoque)
      {
        AdicionarErroProcessamento($"O produto {produto.Descricao} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
        return;
      }

      if (quantidade > produto.QuantidadeEstoque) AdicionarErroProcessamento($"O produto {produto.Descricao} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
    }
  }
}

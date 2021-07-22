using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CBP.WebApp.MVC.Services
{
  public interface ITermosBffService
  {
    // Carrinho
    Task<GuiaViewModel> ObterGuia();
    Task<int> ObterQuantidadeGuia();
    Task<ResponseResult> AdicionarItemGuia(ItemGuiaViewModel guia);
    Task<ResponseResult> AtualizarItemGuia(Guid patrimonioId, ItemGuiaViewModel guia);
    Task<ResponseResult> RemoverItemGuia(Guid patrimonioId);

    // Pedido
    //Task<ResponseResult> FinalizarPedido(PedidoTransacaoViewModel pedidoTransacao);
    //Task<PedidoViewModel> ObterUltimoPedido();
    //Task<IEnumerable<PedidoViewModel>> ObterListaPorClienteId();
    //PedidoTransacaoViewModel MapearParaPedido(CarrinhoViewModel carrinho, EnderecoViewModel endereco);
  }

  public class TermosBffService : Service, ITermosBffService
  {
    private readonly HttpClient _httpClient;

    public TermosBffService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
      _httpClient = httpClient;
      _httpClient.BaseAddress = new Uri(settings.Value.TermosBffUrl);
    }

    #region Guia
    public async Task<GuiaViewModel> ObterGuia()
    {
      var response = await _httpClient.GetAsync("/guia/transferencia/");

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<GuiaViewModel>(response);
    }

    public async Task<int> ObterQuantidadeGuia()
    {
      var response = await _httpClient.GetAsync("/guia/transferencia-quantidade/");

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<int>(response);
    }

    public async Task<ResponseResult> AdicionarItemGuia(ItemGuiaViewModel guia)
    {
      var itemContent = ObterConteudo(guia);

      var response = await _httpClient.PostAsync("/guia/transferencia/items/", itemContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

    public async Task<ResponseResult> AtualizarItemGuia(Guid patrimonioId, ItemGuiaViewModel item)
    {
      var itemContent = ObterConteudo(item);

      var response = await _httpClient.PutAsync($"/guia/transferencia/items/{patrimonioId}", itemContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

    public async Task<ResponseResult> RemoverItemGuia(Guid patrimonioId)
    {
      var response = await _httpClient.DeleteAsync($"/guia/transferencia/items/{patrimonioId}");

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

    #endregion Guia
  }
}
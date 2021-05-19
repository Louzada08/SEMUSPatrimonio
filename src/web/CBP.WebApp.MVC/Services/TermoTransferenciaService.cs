using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CBP.WebApp.MVC.Models;
using CBP.WebApp.MVC.Extensions;
using CBP.Core.Communication;

namespace CBP.WebApp.MVC.Services
{
  public class TermoTransferenciaService : Service, ITermoTransferenciaService
  {
    private readonly HttpClient _httpClient;

    public TermoTransferenciaService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
      _httpClient = httpClient;
      _httpClient.BaseAddress = new Uri(settings.Value.TermoTransferenciaUrl);
    }

    public async Task<TermoTransferenciaViewModel> ObterTermoTransferencia()
    {
      var response = await _httpClient.GetAsync("/termotransferencia");

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<TermoTransferenciaViewModel>(response);
    }

    public async Task<ResponseResult> AdicionarItemTermoTransferencia(ItemPatrimonioViewModel patrimonio)
    {
      var itemContent = ObterConteudo(patrimonio);
                                                  
      var response = await _httpClient.PostAsync("/termotransferencia/", itemContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

    public async Task<ResponseResult> AtualizarItemTermoTransferencia(Guid patrimonioId, ItemPatrimonioViewModel patrimonio)
    {
      var itemContent = ObterConteudo(patrimonio);

      var response = await _httpClient.PutAsync($"/termotransferencia/{patrimonio.PatrimonioId}", itemContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

    public async Task<ResponseResult> RemoverItemTermoTransferencia(Guid patrimonioId)
    {
      var response = await _httpClient.DeleteAsync($"/termotransferencia/{patrimonioId}");

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }
  }
}

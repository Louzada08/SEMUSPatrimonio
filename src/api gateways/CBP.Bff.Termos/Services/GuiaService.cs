using CBP.Bff.Termos.Extensions;
using CBP.Bff.Termos.Models;
using CBP.Core.Communication;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CBP.Bff.Termos.Services
{
  public interface IGuiaService
  {
    Task<GuiaTransferenciaDTO> ObterGuia();
    Task<ResponseResult> AdicionarItemGuia(ItemGuiaDTO patrimonio);
    Task<ResponseResult> AtualizarItemGuia(Guid patrimonioId, ItemGuiaDTO guia);
    Task<ResponseResult> RemoverItemGuia(Guid patrimonioId);
  }

  public class GuiaService : Service, IGuiaService
  {
    private readonly HttpClient _httpClient;

    public GuiaService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
      _httpClient = httpClient;
      _httpClient.BaseAddress = new Uri(settings.Value.TermoTransferenciaUrl);
    }

    public async Task<GuiaTransferenciaDTO> ObterGuia()
    {
      var response = await _httpClient.GetAsync("/guia/");

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<GuiaTransferenciaDTO>(response);
    }

    public async Task<ResponseResult> AdicionarItemGuia(ItemGuiaDTO patrimonio)
    {
      var itemContent = ObterConteudo(patrimonio);

      var response = await _httpClient.PostAsync("/guia/", itemContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

    public async Task<ResponseResult> AtualizarItemGuia(Guid patrimonioId, ItemGuiaDTO guia)
    {
      var itemContent = ObterConteudo(guia);

      var response = await _httpClient.PutAsync($"/guia/{guia.PatrimonioId}", itemContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

    public async Task<ResponseResult> RemoverItemGuia(Guid patrimonioId)
    {
      var response = await _httpClient.DeleteAsync($"/guia/{patrimonioId}");

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }
  }
}
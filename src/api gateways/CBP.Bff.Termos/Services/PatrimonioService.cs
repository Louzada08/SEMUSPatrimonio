using CBP.Bff.Termos.Extensions;
using CBP.Bff.Termos.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CBP.Bff.Termos.Services
{
  public interface IPatrimonioService
  {
    Task<ItemPatrimonioDTO> ObterPorId(Guid id);
    Task<IEnumerable<ItemPatrimonioDTO>> ObterItens(IEnumerable<Guid> ids);
  }

  public class PatrimonioService : Service, IPatrimonioService
  {
    private readonly HttpClient _httpClient;

    public PatrimonioService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
      _httpClient = httpClient;
      _httpClient.BaseAddress = new Uri(settings.Value.PatrimonioUrl);
    }

    public async Task<ItemPatrimonioDTO> ObterPorId(Guid id)
    {
      var response = await _httpClient.GetAsync($"/catalogo/patrimonio/{id}");

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<ItemPatrimonioDTO>(response);
    }

    public async Task<IEnumerable<ItemPatrimonioDTO>> ObterItens(IEnumerable<Guid> ids)
    {
      var idsRequest = string.Join(",", ids);

      var response = await _httpClient.GetAsync($"/catalogo/patrimonio/lista/{idsRequest}/");

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<IEnumerable<ItemPatrimonioDTO>>(response);
    }

  }
}
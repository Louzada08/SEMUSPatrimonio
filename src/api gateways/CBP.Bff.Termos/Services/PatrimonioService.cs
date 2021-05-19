using CBP.Bff.Termos.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace CBP.Bff.Termos.Services
{
  public interface IPatrimonioService
  {
  }

  public class PatrimonioService : Service, IPatrimonioService
  {
    private readonly HttpClient _httpClient;

    public PatrimonioService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
      _httpClient = httpClient;
      _httpClient.BaseAddress = new Uri(settings.Value.PatrimonioUrl);
    }
  }
}
using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using CBP.Bff.Termos.Extensions;

namespace CBP.Bff.Termos.Services
{
    public interface IPagamentoService
    {
    }

    public class PagamentoService : Service, IPagamentoService
    {
        private readonly HttpClient _httpClient;

        public PagamentoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.TermoTransferenciaUrl);
        }
    }
}
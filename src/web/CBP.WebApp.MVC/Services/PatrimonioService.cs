using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Models;

namespace CBP.WebApp.MVC.Services
{
    public class PatrimonioService : Service, IPatrimonioService
    {
        private readonly HttpClient _httpClient;

        public PatrimonioService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.PatrimonioUrl);

            _httpClient = httpClient;
        }

        public async Task<PatrimonioViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/patrimonio/patrimonios/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PatrimonioViewModel>(response);
        }

        public async Task<IEnumerable<PatrimonioViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("/patrimonio/patrimonios/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<PatrimonioViewModel>>(response);
        }
    }
}
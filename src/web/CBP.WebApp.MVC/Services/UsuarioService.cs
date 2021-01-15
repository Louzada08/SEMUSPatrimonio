using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Models;

namespace CBP.WebApp.MVC.Services
{
  public interface IUsuarioService
  {
    Task<ResponsavelViewModel> ObterPorId(Guid id);
    Task<IEnumerable<ResponsavelViewModel>> ObterTodos();
  }

  public class UsuarioService : Service, IUsuarioService
  {
    private readonly HttpClient _httpClient;

    public UsuarioService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
      httpClient.BaseAddress = new Uri(settings.Value.UsuarioUrl);

      _httpClient = httpClient;
    }

    public async Task<ResponsavelViewModel> ObterPorId(Guid id)
    {
      var response = await _httpClient.GetAsync($"/patrimonio/patrimonios/{id}");

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<ResponsavelViewModel>(response);
    }

    public async Task<IEnumerable<ResponsavelViewModel>> ObterTodos()
    {
      var response = await _httpClient.GetAsync("/responsaveis");

      TratarErrosResponse(response);

      var responsavel = await DeserializarObjetoResponse<IEnumerable<ResponsavelViewModel>>(response);

      return responsavel;
    }

  }
}
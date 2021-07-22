using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace CBP.WebApp.MVC.Services
{
  public interface IUsuarioService
  {
    Task<IEnumerable<UsuarioRegistro>> ObterTodos();
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

    public async Task<IEnumerable<UsuarioRegistro>> ObterTodos()
    {
      var response = await _httpClient.GetAsync("/usuarios");

      TratarErrosResponse(response);

      var usuarios = await DeserializarObjetoResponse<IEnumerable<UsuarioRegistro>>(response);

      return usuarios;
    }

  }
}
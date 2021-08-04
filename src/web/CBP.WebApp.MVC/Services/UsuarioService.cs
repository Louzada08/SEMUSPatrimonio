using CBP.WebApp.MVC.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using CBP.WebApp.MVC.Models;

namespace CBP.WebApp.MVC.Services
{
  public interface IUsuarioService
  {
    Task<IEnumerable<IdentityUser>> ObterTodos();
  }

  public class UsuarioService : Service, IUsuarioService
  {
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public UsuarioService(HttpClient httpClient,
        IOptions<AppSettings> settings, IMapper mapper)
    {
      httpClient.BaseAddress = new Uri(settings.Value.UsuarioUrl);

      _httpClient = httpClient;
      _mapper = mapper;
    }

    public async Task<IEnumerable<IdentityUser>> ObterTodos()
    {
      var response = await _httpClient.GetAsync("api/usuario/obter-usuarios");

      TratarErrosResponse(response);

      var usuarios = await DeserializarObjetoResponse<IEnumerable<IdentityUser>>(response);


      return usuarios;

    }

  }
}
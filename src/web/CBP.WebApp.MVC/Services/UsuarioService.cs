using CBP.WebApp.MVC.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using CBP.WebApp.MVC.Models;
using CBP.WebApp.MVC.DTO;

namespace CBP.WebApp.MVC.Services
{
  public interface IUsuarioService
  {
    Task<IEnumerable<IdentityUser>> ObterTodos();
    Task<IEnumerable<IdentityRole>> ObterTodosRoles();
    Task<RoleResposta> RoleRegistro(RoleRegistroViewModel roleRegistro);
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

    #region Roles
    public async Task<IEnumerable<IdentityRole>> ObterTodosRoles()
    {
      var response = await _httpClient.GetAsync("api/usuario/obter-roles");

      TratarErrosResponse(response);

      var roles = await DeserializarObjetoResponse<IEnumerable<IdentityRole>>(response);


      return roles;
    }

    public async Task<RoleResposta> RoleRegistro(RoleRegistroViewModel roleRegistro)
    {
      var registroContent = ObterConteudo(_mapper.Map<IdentityRole>(roleRegistro));
      //var registroContent = ObterConteudo(roleRegistro);

      var response = await _httpClient.PostAsync("/api/usuario/nova-role", registroContent);

      if (!TratarErrosResponse(response))
      {
        return new RoleResposta
        {
          ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
        };
      }

      var resposta = await DeserializarObjetoResponse<RoleResposta>(response);
      return resposta;
    }

    #endregion Roles
  }
}
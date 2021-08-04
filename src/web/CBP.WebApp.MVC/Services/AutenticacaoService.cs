using AutoMapper;
using CBP.WebApp.MVC.DTO;
using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CBP.WebApp.MVC.Services
{
  public interface IAutenticacaoService
  {
    Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);
    Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);

    //Task RealizarLogin(UsuarioRespostaLogin resposta);
    //Task Logout();
    //bool TokenExpirado();
    //Task<bool> RefreshTokenValido();
  }

  public class AutenticacaoService : Service, IAutenticacaoService
  {
    private readonly HttpClient _httpClient;

    private readonly IMapper _mapper;
    //private readonly IAuthenticationService _authenticationService;

    public AutenticacaoService(HttpClient httpClient,
                               IOptions<AppSettings> settings, IMapper mapper)
    {
      httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);

      _httpClient = httpClient;
      _mapper = mapper;
    }

    public async Task<IEnumerable<UsuarioRegistro>> ObterTodosUsuarios()
    {
      var response = await _httpClient.GetAsync("/api/identidade/obtertodosusers");

      TratarErrosResponse(response);

      var usuarios = await DeserializarObjetoResponse<IEnumerable<UsuarioRegistro>>(response);

      return usuarios;
    }

    public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
    {
      var loginContent = ObterConteudo(usuarioLogin);

      var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

      if (!TratarErrosResponse(response))
      {
        return new UsuarioRespostaLogin
        {
          ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
        };
      }

      return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
    }

    public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
    {
      var registroContent = ObterConteudo(_mapper.Map<UsuarioDTO>(usuarioRegistro));

       var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

      if (!TratarErrosResponse(response))
      {
        return new UsuarioRespostaLogin
        {
          ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
        };
      }

      return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
    }

    //public async Task<UsuarioRespostaLogin> UtilizarRefreshToken(string refreshToken)
    //{
    //  var refreshTokenContent = ObterConteudo(refreshToken);

    //  var response = await _httpClient.PostAsync("/api/identidade/refresh-token", refreshTokenContent);

    //  if (!TratarErrosResponse(response))
    //  {
    //    return new UsuarioRespostaLogin
    //    {
    //      ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
    //    };
    //  }

    //  return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
    //}

    //public async Task<bool> RefreshTokenValido()
    //{
    //  var resposta = await UtilizarRefreshToken(_user.ObterUserRefreshToken());

    //  if (resposta.AccessToken != null && resposta.ResponseResult == null)
    //  {
    //    await RealizarLogin(resposta);
    //    return true;
    //  }

    //  return false;
    //}

  }
}
﻿using CBP.WebApp.MVC.Extensions;
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
    Task<UsuarioRespostaLogin> Registro(UsuarioViewModel usuarioRegistro);
    Task<UsuarioRespostaLogin> Atualizacao(UsuarioViewModel usuarioRegistro);
  }

  public class AutenticacaoService : Service, IAutenticacaoService
  {
    private readonly HttpClient _httpClient;

    public AutenticacaoService(HttpClient httpClient,
                               IOptions<AppSettings> settings)
    {
      httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);

      _httpClient = httpClient;
    }

    public async Task<IEnumerable<UsuarioViewModel>> ObterTodosUsuarios()
    {
      var response = await _httpClient.GetAsync("/api/identidade/obtertodosusers");

      TratarErrosResponse(response);

      var usuarios = await DeserializarObjetoResponse<IEnumerable<UsuarioViewModel>>(response);

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

    public async Task<UsuarioRespostaLogin> Registro(UsuarioViewModel usuarioRegistro)
    {
      var registroContent = ObterConteudo(usuarioRegistro);

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

    public Task<UsuarioRespostaLogin> Atualizacao(UsuarioViewModel usuarioRegistro)
    {
      throw new NotImplementedException();
    }
  }
}
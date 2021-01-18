using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Models;

namespace CBP.WebApp.MVC.Services
{
  public interface IResponsavelService
  {
    Task<UsuarioViewModel> ObterUsuarioPorId(Guid id);
    Task<ResponsavelViewModel> ObterPorId(Guid id);
    Task<IEnumerable<ResponsavelViewModel>> ObterTodos();
  }

  public class ResponsavelService : Service, IResponsavelService
  {
    private readonly HttpClient _httpClient;

    public ResponsavelService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
      httpClient.BaseAddress = new Uri(settings.Value.ResponsavelUrl);

      _httpClient = httpClient;
    }

    public async Task<UsuarioViewModel> ObterUsuarioPorId(Guid id)
    {
      var response = await _httpClient.GetAsync($"/usuario/{id}");

      TratarErrosResponse(response);

      var responsavel = await DeserializarObjetoResponse<ResponsavelViewModel>(response);

      UsuarioViewModel usuarioViewModel = new UsuarioViewModel
      {
        Nome = responsavel.Nome,
        Email = responsavel.Email.Endereco,
        Funcao = (Funcao)Enum.Parse(typeof(Funcao), responsavel.Funcao),
        Senha = "",
        SenhaConfirmacao = "",
        Excluido = responsavel.Excluido
      };

      return usuarioViewModel;
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
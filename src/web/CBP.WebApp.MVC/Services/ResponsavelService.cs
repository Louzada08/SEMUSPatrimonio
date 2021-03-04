using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Models;
using System.Collections.Generic;

namespace CBP.WebApp.MVC.Services
{
  public interface IResponsavelService
  {
    Task<IEnumerable<ResponsavelViewModel>> ObterTodosUsuarios();
    Task<ResponsavelViewModel> ObterResponsavelPorId(Guid id);
    Task<ResponsavelViewModel> ObterPorId(Guid id);
    Task<ResponseResult> Atualizacao(ResponsavelViewModel responsavelViewModel);
    Task<ResponseResult> AdicionarEndereco(EnderecoViewModel endereco);
  }

  public class ResponsavelService : Service, IResponsavelService
  {
    private readonly HttpClient _httpClient;

    public ResponsavelService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
      _httpClient = httpClient;
      httpClient.BaseAddress = new Uri(settings.Value.ResponsavelUrl);
    }

    public async Task<IEnumerable<ResponsavelViewModel>> ObterTodosUsuarios()
    {
      var responsaveis = await _httpClient.GetAsync("/responsaveis");

      TratarErrosResponse(responsaveis);

      return await DeserializarObjetoResponse<IEnumerable<ResponsavelViewModel>>(responsaveis);
    }

    public async Task<ResponsavelViewModel> ObterResponsavelPorId(Guid id)
    {
      var response = await _httpClient.GetAsync($"/responsavel/{id}");

      TratarErrosResponse(response);

      var responsavel = await DeserializarObjetoResponse<ResponsavelViewModel>(response);

      return responsavel;
    }

    public async Task<ResponsavelViewModel> ObterPorId(Guid id)
    {
      var response = await _httpClient.GetAsync($"/patrimonio/patrimonios/{id}");

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<ResponsavelViewModel>(response);
    }

    public async Task<ResponseResult> Atualizacao(ResponsavelViewModel responsavel)
    {
      var responsavelContent = ObterConteudo(responsavel);

      var response = await _httpClient.PutAsync($"/responsavel-editar/{responsavel.Id}", responsavelContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

    public async Task<ResponseResult> AdicionarEndereco(EnderecoViewModel endereco)
    {
      var enderecoContent = ObterConteudo(endereco);

      var response = await _httpClient.PostAsync("/responsavel/endereco/", enderecoContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

  }
}
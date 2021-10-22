using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Models;
using System.Collections.Generic;
using AutoMapper;
using CBP.WebApp.MVC.DTO;

namespace CBP.WebApp.MVC.Services
{
  public interface IResponsavelService
  {
    Task<IEnumerable<ResponsavelViewModel>> ObterTodosResponsaveis();
    Task<ResponsavelViewModel> ObterResponsavelPorId(Guid id);
    Task<ResponsavelViewModel> ObterPorId(Guid id);
    Task<ResponseResult> Atualizacao(ResponsavelViewModel responsavel);
    Task<ResponseResult> AdicionarEndereco(EnderecoViewModel endereco);
  }

  public class ResponsavelService : Service, IResponsavelService
  {
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public ResponsavelService(HttpClient httpClient,
        IOptions<AppSettings> settings, IMapper mapper)
    {
      _httpClient = httpClient;
      httpClient.BaseAddress = new Uri(settings.Value.ResponsavelUrl);
      _mapper = mapper;
    }

    public async Task<IEnumerable<ResponsavelViewModel>> ObterTodosResponsaveis()
    {
      var responsaveis = await _httpClient.GetAsync("/responsaveis");

      TratarErrosResponse(responsaveis);

      var responsaveisDeser = await DeserializarObjetoResponse<IEnumerable<ResponsavelViewModel>>(responsaveis);

      return _mapper.Map<IEnumerable<ResponsavelViewModel>>(responsaveisDeser);
    }

    public async Task<ResponsavelViewModel> ObterResponsavelPorId(Guid id)
    {
      var response = await _httpClient.GetAsync($"/responsavel/{id}");

      TratarErrosResponse(response);

      var responsavel = await DeserializarObjetoResponse<ResponsavelViewModel>(response);

      var r = _mapper.Map<ResponsavelViewModel>(responsavel);
      return r;
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

      var response = await _httpClient.PutAsync($"/responsavel-editar/", responsavelContent);
      // var response = await _httpClient.PutAsync($"/responsavel-editar/{usuario.Id}", responsavelContent);

      if(!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

    public async Task<ResponseResult> AdicionarEndereco(EnderecoViewModel endereco)
    {
      var enderecoDTO = _mapper.Map<EnderecoDTO>(endereco);

      var enderecoContent = ObterConteudo(enderecoDTO);

      var response = await _httpClient.PostAsync("/responsavel/endereco/", enderecoContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
     // return null;
    }

  }
}
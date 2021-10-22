using AutoMapper;
using CBP.Core.Mediator;
using CBP.ResponsavelPatrimonial.API.DTO;
using CBP.ResponsavelPatrimonial.API.Models;
using CBP.WebAPI.Core.Controllers;
using CBP.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBP.ResponsavelPatrimonial.API.Controllers
{
  public class ResponsavelController : MainController
  {
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;
    private readonly IAspNetUser _user;

    public ResponsavelController(IMediatorHandler mediator, IResponsavelRepository responsavelRepository,
      IMapper mapper, IAspNetUser user)
    {
      _responsavelRepository = responsavelRepository;
      _mediator = mediator;
      _mapper = mapper;
      _user = user;
    }

    [HttpGet("responsavel/{id}")]
    public async Task<IActionResult> ObterResponsavelId(Guid id)
    {
      var responsavel = _mapper.Map<ResponsavelDTO>(await _responsavelRepository.GetResponsavelId(id));

      return responsavel == null ? NotFound() : CustomResponse(responsavel);
    }
    
    [HttpGet("responsaveis")]
    public async Task<IEnumerable<ResponsavelDTO>> ObterListaResponsaveis()
    {
      var responsaveis = _mapper.Map<IEnumerable<ResponsavelDTO>>(await _responsavelRepository.ObterTodos());
      return responsaveis;
    }

    [HttpPut("responsavel-editar")]
    public async Task<IActionResult> EditarResponsavel(ResponsavelDTO responsavelEdita)
    {

      _responsavelRepository.Atualizar(_mapper.Map<Responsavel>(responsavelEdita));

      return CustomResponse(responsavelEdita);
    }

    [HttpGet("responsavel/endereco")]
    public async Task<IActionResult> ObterEndereco()
    {
      var endereco = await _responsavelRepository.ObterEnderecoPorId(_user.ObterUserId());

      return endereco == null ? NotFound() : CustomResponse(endereco);
    }

    [HttpPost("responsavel/endereco")]
    public async Task<IActionResult> AdicionarEndereco(EnderecoDTO endDTO)
    {
      Endereco endereco = _mapper.Map<Endereco>(new Endereco(endDTO.Logradouro, endDTO.Numero, endDTO.Complemento,
                      endDTO.Bairro, endDTO.Cep, endDTO.Cidade, endDTO.Estado, endDTO.ResponsavelId));

      var userLogado = _user.ObterUserId();
      endereco.Id = endereco.ResponsavelId;
      var status = _responsavelRepository.AdicionarEndereco(endereco);
      return CustomResponse(endereco);
    }

  }
}
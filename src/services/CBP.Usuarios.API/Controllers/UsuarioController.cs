using AutoMapper;
using CBP.Core.Mediator;
using CBP.Usuarios.API.DTO;
using CBP.Usuarios.API.Models;
using CBP.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBP.Usuarios.API.Controllers
{
  public class UsuarioController : MainController
  {
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;

    public UsuarioController(IMediatorHandler mediator, IUsuarioRepository usuarioRepository, IMapper mapper)
    {
      _mediator = mediator;
      _mapper = mapper;
      _usuarioRepository = usuarioRepository;
    }

    [HttpGet("usuario/{id}")]
    public async Task<IActionResult> ObterUsuarioId(Guid id)
    {
      var usuario = _mapper.Map<UsuarioDTO>(await _usuarioRepository.GetResponsavelId(id));

      return usuario == null ? NotFound() : CustomResponse(usuario);
    }

    [HttpGet("usuarios")]
    public async Task<IEnumerable<UsuarioDTO>> ObterListaUsuarios()
    {
      return await _usuarioRepository.ObterTodos();
    }

    [HttpPut("usuario-editar")]
    public async Task<IActionResult> EditarUsuario(UsuarioDTO usuarioEdita)
    {

      _usuarioRepository.Atualizar(_mapper.Map<Usuario>(usuarioEdita));

      return CustomResponse(usuarioEdita);
    }

  }
}
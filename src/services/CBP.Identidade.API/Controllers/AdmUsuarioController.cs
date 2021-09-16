using CBP.Identidade.API.Models;
using CBP.WebAPI.Core.Controllers;
using CBP.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBP.Core.Messages.Integration;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBP.Identidade.API.Controllers
{
  [Route("api/usuario")]
  public partial class AuthController
  {
    //private readonly RoleManager<IdentityRole> _roleManager;
    //private readonly UserManager<IdentityUser> _userManager;

    //public AdmUsuarioController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    //{
    //  _roleManager = roleManager;
    //  _userManager = userManager;
    //}

    // GET: api/usuario/obter-usuario/{id}
    [HttpGet("obter-usuario/{id}")]
    public async Task<UsuarioRegistro> ObtemUserId(string id)
    {
      var usuario = await _userManager.FindByIdAsync(id);

      var userClaims = await _userManager.GetClaimsAsync(usuario);

      if (usuario.AccessFailedCount == 0)
      {
        var usuarioRegistro = new UsuarioRegistro
        {
          Email = usuario.Email,
          Nome = usuario.UserName,
          Funcao = Funcao.ObterEnumNomePeloId(int.Parse(userClaims.Select(c => c.Value).FirstOrDefault()))
        };
        return usuarioRegistro;
      }

      return null;
    }

    [HttpGet("obter-identityuser/{id}")]
    public async Task<IdentityUser> ObtemUserId(Guid id)
    {
      var usuario = await _userManager.FindByIdAsync(id.ToString());

      if (usuario == null)
      {
        return null;
      }

      return usuario;
    }

    // GET: api/usuario/obter-usuarios
    [HttpGet("obter-usuarios")]
    public IEnumerable<IdentityUser> ObtemUsuarios()
    {
      var usuarios = _userManager.Users;
      return usuarios;
    }

    
    [HttpPost("atualiza")]
    public async Task<ActionResult> Atualizar(UsuarioRegistro usuario)
    {
      if (!ModelState.IsValid) return CustomResponse(ModelState);

      var user = _userManager.FindByEmailAsync(usuario.Email).Result;

      if (user == null || !_userManager.IsEmailConfirmedAsync(user).Result)
      {
        return CustomResponse(NotFound().StatusCode);
      }

      user.UserName = usuario.Nome;

      var token = await _userManager.GenerateChangeEmailTokenAsync(user,usuario.Nome);
      var result = await _userManager.ChangeEmailAsync(user, usuario.Nome, token);
      var resultBloqueio = await _userManager.SetLockoutEnabledAsync(user,usuario.Excluido);

      if (result.Succeeded && resultBloqueio.Succeeded)
      {
        return CustomResponse();
      }

      foreach (var error in result.Errors)
      {
        AdicionarErroProcessamento(error.Description);
      }

      return CustomResponse();
    }

    [HttpPost("resetpsw")]
    public async Task<ActionResult> ResetSenha(ResetSenhaViewModel usuario)
    {
      if (!ModelState.IsValid) return CustomResponse(ModelState);

      var user = _userManager.FindByEmailAsync(usuario.UserName).Result;

      if (user == null || !_userManager.IsEmailConfirmedAsync(user).Result)
      {
        return CustomResponse(NotFound().StatusCode);
      }

      var token = await _userManager.GeneratePasswordResetTokenAsync(user);
      var result = await _userManager.ResetPasswordAsync(user, token, usuario.NewPassword);

      if (result.Succeeded)
      {
        return CustomResponse();
      }

      foreach (var error in result.Errors)
      {
        AdicionarErroProcessamento(error.Description);
      }

      return CustomResponse();
    }


    // GET api/usuario/obter-roles
    [HttpGet("obter-roles")]
    public IEnumerable<IdentityRole> ObtemRoles()
    {
      return _roleManager.Roles;
    }

    // GET api/usuario/nova-roles
    [HttpPost("nova-role")]
    public async Task<ActionResult> RegistrarRole(FuncaoRegistro funcaoRegistro)
    {
      if (!ModelState.IsValid) return CustomResponse(ModelState);

      var role = new IdentityRole
      {
        Name = funcaoRegistro.FuncaoNome
      };

      var result = await _roleManager.CreateAsync(role);

      if (result.Succeeded)
      {
        var funcao = Funcao.ObterEnumIdPeloNome(funcaoRegistro.FuncaoNome);
      }

      foreach (var error in result.Errors)
      {
        AdicionarErroProcessamento(error.Description);
      }

      return CustomResponse();
    }

    // POST api/<AdmUsuarioController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<AdmUsuarioController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<AdmUsuarioController>/5
    [HttpDelete("delete-usuario/{id}")]
    public async Task<ActionResult> Delete(string id)
    {
      var user = await _userManager.FindByIdAsync(id);

      if (user == null)
      {
        return CustomResponse(ModelState.Values);
      }

      var result = await _userManager.DeleteAsync(user);

      if (result.Succeeded) return CustomResponse();

      foreach (var error in result.Errors)
      {
        AdicionarErroProcessamento(error.Description);
      }

      return CustomResponse();
    }

    private async Task<ResponseMessage> ObterResponsavel(IdentityUser usuario)
    {
      var usuarioRegistrado = new ResponsavelIntegrationEvent(Guid.Parse(usuario.Id), "");

      try
      {
        return await _bus.RequestAsync<ResponsavelIntegrationEvent, ResponseMessage>(usuarioRegistrado);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

  }
}

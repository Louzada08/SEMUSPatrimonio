using CBP.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBP.Identidade.API.Controllers
{
  [Route("api/usuario")]
  public class AdmUsuarioController : MainController
  {
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AdmUsuarioController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      _roleManager = roleManager;
      _userManager = userManager;
    }

    // GET: api/usuario/obter-usuarios
    [HttpGet("obter-usuarios")]
    public  IEnumerable<IdentityUser> ObtemUsuarios()
    {
      var usuarios = _userManager.Users;
      return usuarios;
    }

    // GET api/usuario/obter-roles
    [HttpGet("obter-funcoes")]
    public IEnumerable<IdentityRole> ObtemRoles()
    {
      return _roleManager.Roles;
    }

    // GET api/usuario/nova-roles
    [HttpGet("nova-funcao")]
    public async Task<ActionResult> RegistrarRole(FuncaoRegistro funcaoRegistro)
    {
      if (!ModelState.IsValid) return CustomResponse(ModelState);

      var funcao = new IdentityRole
      {
        Name = funcaoRegistro.Nome
      };

      var result = await _roleManager.CreateAsync(funcao);

      if (result.Succeeded)
      {

        var funcao = Funcao.ObterEnumIdPeloNome(usuarioRegistro.Funcao);

        var responsavelResult = await RegistrarResponsavel(usuarioRegistro);

        if (!responsavelResult.ValidationResult.IsValid)
        {
          await _userManager.DeleteAsync(user);
          return CustomResponse(responsavelResult.ValidationResult);
        }

        await _userManager.AddClaimAsync(user, new Claim("NivelDeAcesso", funcao.ToString("d2")));

        return CustomResponse(await GerarJwt(usuarioRegistro.Email));
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
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}

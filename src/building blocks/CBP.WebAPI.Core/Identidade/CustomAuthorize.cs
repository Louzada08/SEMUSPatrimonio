using System;
using System.Linq;
using System.Security.Claims;
using CBP.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CBP.WebAPI.Core.Identidade
{
  public class CustomAuthorization
  {
    public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
    {
      var nivelDeAcesso = ObtemNivelDeAcesso(claimValue);

      return context.User.Identity.IsAuthenticated &&
             context.User.Claims.Any(c => c.Type == claimName && short.Parse(c.Value) >= nivelDeAcesso);
    }

    private static short ObtemNivelDeAcesso(string claimValue)
    {
      foreach(var item in Enum.GetValues(typeof(Funcao)).Cast<Funcao>())
      {
        if (item.ToString().Contains(claimValue)) return (short)item;
      }
      return 1;
    }
  }

  public class ClaimsAuthorizeAttribute : TypeFilterAttribute
  {
    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
    {
      Arguments = new object[] { new Claim(claimName, claimValue) };
    }
  }

  public class RequisitoClaimFilter : IAuthorizationFilter
  {
    private readonly Claim _claim;

    public RequisitoClaimFilter(Claim claim)
    {
      _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      if (!context.HttpContext.User.Identity.IsAuthenticated)
      {
        context.Result = new StatusCodeResult(401);
        return;
      }

      if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
      {
        context.Result = new StatusCodeResult(403);
      }
    }
  }
}
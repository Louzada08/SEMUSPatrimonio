using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CBP.WebApp.MVC.Models
{
  public class RoleRegistroViewModel
  {
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Função")]
    public string FuncaoNome { get; set; }
  }

  public class FuncaoEditarViewModel
  {
    public FuncaoEditarViewModel()
    {
      Users = new List<string>();
    }

    public string Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string FuncaoNome { get; set; }
    public List<string> Users { get; set; }
  }

  public class RoleResposta
  {
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public RoleToken RoleToken { get; set; }
    public ResponseResult ResponseResult { get; set; }
  }

  public class RoleToken
  {
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UsuarioClaim> Claims { get; set; }
  }

  public class RoleClaim
  {
    public string Value { get; set; }
    public string Type { get; set; }
  }


}
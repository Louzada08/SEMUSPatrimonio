using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CBP.Identidade.API.Models
{
  public class FuncaoRegistro
  {
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name ="Função")]
    public string FuncaoNome { get; set; }
  }

  public class FuncaoEditar
  {
    public FuncaoEditar()
    {
      Users = new List<string>();
    }

    public string Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string FuncaoNome { get; set; }
    public List<string> Users { get; set; }
  }
}
using System;

namespace CBP.WebApp.MVC.DTO
{
  public class UsuarioDTO
  {
    //public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Funcao { get; set; }
    public bool Excluido { get; set; }
    public string Senha { get; set; }
    public string SenhaConfirmacao { get; set; }
  }
}
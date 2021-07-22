using CBP.Core.DomainObjects;
using System;
using System.Linq;

namespace CBP.WebAPI.Core.Usuario
{
  public enum Funcoes
  {
    Desenvolvedor = 4,
    Administrador = 3,
    Responsavel = 2,
    Operador = 1,
  }

  public class Funcao
  {
    public string Nome { get; private set; }
    public int Codigo { get; private set; }

    public Funcao() { }

    public Funcao(int codigo = 0)
    {
      if (!ValidarCodigo(codigo)) throw new DomainException("Função inválida");
      var funcao = (Funcoes)codigo;
      Nome = ObterEnumNomePeloId(codigo);
    }

    public Funcao(string nome = "")
    {
      if (!ValidarNome(nome)) throw new DomainException("Função inválida");
      Codigo = ObterEnumIdPeloNome(nome);
    }

    public static int ObterEnumIdPeloNome(string nome)
    {
      var funcao = (Funcoes)Enum.Parse(typeof(Funcoes), nome);
      return (int)funcao;
    }

    public static string ObterEnumNomePeloId(int codigo)
    {
      var funcao = (Funcoes)codigo;
      return funcao.ToString();
    }

    public static bool ValidarNome(string nome)
    {
      bool eValido = false;
      foreach (var valor in Enum.GetNames(typeof(Funcoes)))
      {
        if (valor == nome)
        {
          eValido = true;
          return eValido;
        }
      }
      return eValido;
    }

    public static bool ValidarCodigo(int codigo)
    {
      bool eValido = false;
      foreach (var valor in Enum.GetValues(typeof(Funcoes)).Cast<Funcoes>())
      {
        if (valor.Equals(codigo))
        {
          eValido = true;
          return eValido;
        }
      }
      return eValido;
    }

    public static object RetornaEnumPeloNome(string nome)
    {
      foreach (var valor in Enum.GetNames(typeof(Funcoes)))
      {
        if (valor == nome)
        {
          return Enum.Parse(typeof(Funcoes), valor);
        }
      }
      return null;
    }
  } 
}

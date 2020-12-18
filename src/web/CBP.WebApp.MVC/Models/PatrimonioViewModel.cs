using System;

namespace CBP.WebApp.MVC.Models
{
  public class PatrimonioViewModel
  {
    public Guid Id { get; set; }
    public int CodigoPatrimonio { get; set; }
    public string Descricao { get; set; }
    public Guid EstadoConservacaoId { get; set; }
    public int? NumeroNotaFiscal { get; set; }
    public decimal ValorBem { get; set; }
    public int QuantidadeEstoque { get; set; }
  }
}
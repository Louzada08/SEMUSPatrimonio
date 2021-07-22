using System;
using System.Collections.Generic;

namespace CBP.WebApp.MVC.Models
{
  public class GuiaViewModel
  {
    public DateTime DataEmissao { get; set; }
    public DateTime DataRecebimento { get; set; }
    public DateTime DataVistoSetorDePatrimonio { get; set; }
    public string Motivo { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ItemGuiaViewModel> Itens { get; set; } = new List<ItemGuiaViewModel>();
  }

  public class ItemGuiaViewModel
  {
    public Guid PatrimonioId { get; set; }
    public int CodigoPatrimonio { get; set; }
    public int? CodigoPatrimonioCP { get; set; }
    public string Descricao { get; set; }
    public Guid EstadoConservacaoId { get; set; }
    public int? NumeroNotaFiscal { get; set; }
    public Guid LocalId { get; set; }
    public string LocalComplemento { get; set; }
    public DateTime DataEntrada { get; set; }
    public DateTime? DataTransferencia { get; set; }
    public DateTime? DataDoacao { get; set; }
    public DateTime? DataEmprestimo { get; set; }
    public DateTime? DataRetornoEmprestimo { get; set; }
    public DateTime? DataBaixa { get; set; }
    public int QuantidadeEstoque { get; set; }
    public decimal ValorBem { get; set; }
    public int NumeroProcessoBaixa { get; set; }
    public int CodigoDaBaixa { get; set; }
    public bool Ativo { get; set; }
  }
}
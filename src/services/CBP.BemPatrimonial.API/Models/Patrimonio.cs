using CBP.Core.DomainObjects;
using System;

namespace CBP.BemPatrimonial.API.Models
{
  public class Patrimonio : Entity, IAggregateRoot
  {
    public int CodigoPatrimonio { get; set; }
    public int? CodigoPatrimonioCP { get; set; }
    public string Descricao { get; set; }
    public Guid EstadoConservacaoId { get; set; }
    public EstadoConservacao EstadoConservacoes { get; set; }
    public int? NumeroNotaFiscal { get; set; }
    public Guid LocalId { get; set; }
    public Local Locais { get; set; }
    public string LocalComplemento { get; set; }
    public DateTime DataEntrada { get; set; }
    public DateTime? DataTransferencia { get; set; }
    public DateTime? DataDoacao { get; set; }
    public DateTime? DataEmprestimo { get; set; }
    public DateTime? DataRetornoEmprestimo { get; set; }
    public DateTime? DataBaixa { get; set; }
    public decimal ValorBem { get; set; }
    public Guid? PessoaResponsavelId { get; set; }
    public Pessoa PessoasResponsaveis { get; set; }
    public int NumeroProcessoBaixa { get; set; }
    public int CodigoDaBaixa { get; set; }
    public bool Ativo { get; set; }
  }
}

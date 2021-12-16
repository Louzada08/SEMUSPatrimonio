using CBP.Core.DomainObjects;
using System;

namespace CBP.BemPatrimonial.API.Models
{
  public class Patrimonio : Entity, IAggregateRoot
  {
    public int CodigoPatrimonio { get; private set; }
    public int? CodigoPatrimonioCP { get; private set; }
    public string Descricao { get; private set; }
    public EstadoConservacao EstadoConservacao { get; private set; }
    public int? NumeroNotaFiscal { get; private set; }
    public Guid LocalId { get; private set; }
    public Local Local { get; private set; }
    public DateTime DataEntrada { get; private set; }
    public DateTime? DataTransferencia { get; private set; }
    public DateTime? DataDoacao { get; private  set; }
    public DateTime? DataEmprestimo { get; private set; }
    public DateTime? DataRetornoEmprestimo { get; private set; }
    public DateTime? DataBaixa { get; private set; }
    public Guid? ResponsavelId { get; private set; }
    public Responsavel Responsavel { get; private set; }
    public int NumeroProcessoBaixa { get; private set; }
    public int CodigoDaBaixa { get; private set; }
    public bool Ativo { get; private set; }

    protected Patrimonio() { }
    public Patrimonio(int codigoPatrimonio, string descricao, int numeroNotaFiscal, DateTime dataEntrada,
                      EstadoConservacao estadoConservacao, Guid localId, Guid responsalvelId, bool ativo)
    {
      CodigoPatrimonio = codigoPatrimonio;
      Descricao = descricao;
      NumeroNotaFiscal = numeroNotaFiscal;
      DataEntrada = dataEntrada;
      EstadoConservacao = estadoConservacao;
      LocalId = localId;
      ResponsavelId = responsalvelId;
      Ativo = ativo;
    }

    public void Ativar() => Ativo = true;
    public void Desativar() => Ativo = false;
    public void AlterarLocal(Local local)
    {
      Local = local;
      LocalId = local.Id;
    }
    public void AlterarResponsavel(Responsavel responsavel)
    {
      Responsavel = responsavel;
      ResponsavelId = responsavel.Id;
    }

    public void AlterarDataTransferencia(DateTime dataTransferencia)
    {
      if (dataTransferencia < DataEntrada) throw new DomainException("Data de Transferência não ser menor que Data de Entrada");
      DataTransferencia = dataTransferencia;
    }

  }
}

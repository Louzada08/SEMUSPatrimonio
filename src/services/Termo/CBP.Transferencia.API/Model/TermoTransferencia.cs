using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace CBP.Transferencia.API.Model
{
  public class TermoTransferencia
  {
    internal const int MAX_QUANTIDADE_ITEM = 25;

    public Guid Id { get; set; }
    public Guid ResponsavelCedenteId { get; set; }

    //public Guid ResponsavelRecebedorId { get; set; }
    //public Guid LocalCedenteId { get; set; }
    //public Guid LocalRecebedorId { get; set; }
    public DateTime DataEmissao { get; set; }
    public DateTime DataRecebimento { get; set; }
    public DateTime DataVistoSetorDePatrimonio { get; set; }
    public string Motivo { get; set; }
    public decimal ValorTotal { get; set; }
    public List<TermoTransferenciaItem> Itens { get; set; } = new List<TermoTransferenciaItem>();
    public ValidationResult ValidationResult { get; set; }

    public TermoTransferencia(Guid responsavelCedenteId)
    {
      Id = Guid.NewGuid();
      ResponsavelCedenteId = responsavelCedenteId;
    }

    public TermoTransferencia() { }

    internal void CalcularValorTermoTransferencia()
    {
      ValorTotal = Itens.Sum(p => p.CalcularValor());
    }

    internal bool TermoTransferenciaItemExistente(TermoTransferenciaItem item)
    {
      return Itens.Any(p => p.PatrimonioId == item.PatrimonioId);
    }

    internal TermoTransferenciaItem ObterPorPatrimonioId(Guid patrimonioId)
    {
      return Itens.FirstOrDefault(p => p.PatrimonioId == patrimonioId);
    }

    internal void AdicionarItem(TermoTransferenciaItem item)
    {
      item.AssociarTermoTransferencia(Id);

      if (TermoTransferenciaItemExistente(item))
      {
        var itemExistente = ObterPorPatrimonioId(item.PatrimonioId);
        itemExistente.AdicionarUnidades(1);

        item = itemExistente;
        Itens.Remove(itemExistente);
      }

      Itens.Add(item);
      CalcularValorTermoTransferencia();
    }

    internal void AtualizarItem(TermoTransferenciaItem item)
    {
      item.AssociarTermoTransferencia(Id);

      var itemExistente = ObterPorPatrimonioId(item.PatrimonioId);

      Itens.Remove(itemExistente);
      Itens.Add(item);

      CalcularValorTermoTransferencia();
    }

    internal void AtualizarUnidades(TermoTransferenciaItem item, int unidades)
    {
      item.AtualizarUnidades(unidades);
      AtualizarItem(item);
    }

    internal void RemoverItem(TermoTransferenciaItem item)
    {
      Itens.Remove(ObterPorPatrimonioId(item.PatrimonioId));
      CalcularValorTermoTransferencia();
    }

    internal bool EhValido()
    {
      var erros = Itens.SelectMany(i => new TermoTransferenciaItem.ItemTermoTransferenciaValidation().Validate(i).Errors).ToList();
      erros.AddRange(new TermoTransferenciaValidation().Validate(this).Errors);
      ValidationResult = new ValidationResult(erros);

      return ValidationResult.IsValid;
    }

    public class TermoTransferenciaValidation : AbstractValidator<TermoTransferencia>
    {
      public TermoTransferenciaValidation()
      {
        RuleFor(c => c.ResponsavelCedenteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Responsavel Cedente não reconhecido");

        RuleFor(c => c.Itens.Count)
            .GreaterThan(0)
            .WithMessage("O TermoTransferencia não possui itens");

        RuleFor(c => c.ValorTotal)
            .GreaterThan(0)
            .WithMessage("O valor total do TermoTransferencia precisa ser maior que 0");
      }
    }
  }
}
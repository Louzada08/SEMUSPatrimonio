using System;
using System.Text.Json.Serialization;
using FluentValidation;

namespace CBP.Transferencia.API.Model
{
  public class TermoTransferenciaItem
  {
    public TermoTransferenciaItem()
    {
      Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public Guid PatrimonioId { get; set; }
    public string NumeroPatrimonio { get; set; }
    public string NumeroPatrimonioCP { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }
    public decimal Valor { get; set; }

    public Guid TermoTransferenciaId { get; set; }

    [JsonIgnore]
    public TermoTransferencia TermoTransferencia { get; set; }

    internal void AssociarTermoTransferencia(Guid termoTransferenciaId)
    {
      TermoTransferenciaId = termoTransferenciaId;
    }

    internal decimal CalcularValor()
    {
      return Quantidade * Valor;
    }

    internal void AdicionarUnidades(int unidades)
    {
      Quantidade += unidades;
    }

    internal void AtualizarUnidades(int unidades)
    {
      Quantidade = unidades;
    }

    internal bool EhValido()
    {
      return new ItemTermoTransferenciaValidation().Validate(this).IsValid;
    }

    public class ItemTermoTransferenciaValidation : AbstractValidator<TermoTransferenciaItem>
    {
      public ItemTermoTransferenciaValidation()
      {
        RuleFor(c => c.PatrimonioId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do patrimônio inválido");

        RuleFor(c => c.Descricao)
            .NotEmpty()
            .WithMessage("O nome do patrimônio não foi informado");

        RuleFor(c => c.Quantidade)
            .GreaterThan(0)
            .WithMessage(item => $"A quantidade miníma para o {item.Descricao} é 1");

        RuleFor(c => c.Quantidade)
            .LessThanOrEqualTo(TermoTransferencia.MAX_QUANTIDADE_ITEM)
            .WithMessage(item => $"A quantidade máxima do {item.Descricao} é {TermoTransferencia.MAX_QUANTIDADE_ITEM}");

        RuleFor(c => c.Valor)
            .GreaterThan(0)
            .WithMessage(item => $"O valor do {item.Descricao} precisa ser maior que 0");
      }
    }
  }
}
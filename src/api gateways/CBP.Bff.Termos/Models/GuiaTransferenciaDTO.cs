using System;
using System.Collections.Generic;

namespace CBP.Bff.Termos.Models
{
  public class GuiaTransferenciaDTO
  {
    public DateTime DataEmissao { get; set; }
    public DateTime DataRecebimento { get; set; }
    public DateTime DataVistoSetorDePatrimonio { get; set; }
    public string Motivo { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ItemGuiaDTO> Itens { get; set; } = new List<ItemGuiaDTO>();
  }
}
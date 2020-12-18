using System;
using System.Collections.Generic;

namespace CBP.WebApp.MVC.Models
{
  public class TermoTransferenciaViewModel
  {
    public decimal ValorTotal { get; set; }
    public List<ItemPatrimonioViewModel> Itens { get; set; } = new List<ItemPatrimonioViewModel>();
  }

  public class ItemPatrimonioViewModel
  {
    public Guid PatrimonioId { get; set; }
    public string NumeroPatrimonio { get; set; }
    public string NumeroPatrimonioCP { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }
    public decimal Valor { get; set; }
  }
}
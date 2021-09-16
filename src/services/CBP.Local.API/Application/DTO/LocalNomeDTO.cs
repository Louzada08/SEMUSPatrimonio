using System;

namespace CBP.Local.API.Application.DTO
{
  public class LocalNomeDTO
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public LocalNomeDTO() { }
    public LocalNomeDTO(Guid id, string nome)
    {
      Id = id;
      Nome = nome;
    }
  }
}

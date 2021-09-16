using CBP.Local.API.Application.DTO;
using CBP.Local.API.Models;
using System;
using System.Threading.Tasks;

namespace CBP.Local.API.Application.Queries
{
  public interface ILocalQueries
  {
    Task<LocalNomeDTO> ObterNomeLocalPorId(Guid id);
  }

  public class LocalQueries : ILocalQueries
  {
    private readonly ILocalRepository _localRepository;

    public LocalQueries(ILocalRepository localRepository)
    {
      _localRepository = localRepository;
    }

    public async Task<LocalNomeDTO> ObterNomeLocalPorId(Guid id)
    {
      var responsavel = await _localRepository.GetLocalId(id);

      if (responsavel == null) return null;

      return new LocalNomeDTO
      {
        Id = id,
        Nome = responsavel.Nome
      };

    }
  }
}

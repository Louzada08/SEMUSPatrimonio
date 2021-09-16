using CBP.ResponsavelPatrimonial.API.Application.DTO;
using CBP.ResponsavelPatrimonial.API.Models;
using System;
using System.Threading.Tasks;

namespace CBP.ResponsavelPatrimonial.API.Application.Queries
{
  public interface IResponsavelQueries
  {
    Task<ResponsavelNomeDTO> ObterNomeResponsavelPorId(Guid id);
  }

  public class ResponsavelQueries : IResponsavelQueries
  {
    private readonly IResponsavelRepository _responsavelRepository;

    public ResponsavelQueries(IResponsavelRepository responsavelRepository)
    {
      _responsavelRepository = responsavelRepository;
    }

    public async Task<ResponsavelNomeDTO> ObterNomeResponsavelPorId(Guid id)
    {
      var responsavel = await _responsavelRepository.GetResponsavelId(id);

      if (responsavel == null) return null;

      return new ResponsavelNomeDTO
      {
        Id = id,
        Nome = responsavel.Nome
      };

    }
  }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBP.Core.Data;
using CBP.Usuarios.API.DTO;

namespace CBP.Usuarios.API.Models
{
  public interface IUsuarioRepository : IRepository<Usuario>
  {
    void Adicionar(Usuario usuario);
    void Atualizar(Usuario usuario);
    void Remover(Usuario usuario);

    Task<Usuario> GetResponsavelId(Guid id);
    Task<IEnumerable<UsuarioDTO>> ObterTodos();
    Task<Usuario> ObterPorEmail(string email);
  }
}
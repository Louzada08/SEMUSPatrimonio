using CBP.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CBP.Usuarios.API.Data
{
  public sealed class UsuarioContext : DbContext, IUnitOfWork
  {

    public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) 
    {
      ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
      ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public async Task<bool> Commit()
    {
      var sucesso = await base.SaveChangesAsync() > 0;

      return sucesso;
    }
  }

}

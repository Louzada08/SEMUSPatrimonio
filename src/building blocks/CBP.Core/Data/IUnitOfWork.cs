using System.Threading.Tasks;

namespace CBP.Core.Data
{
  public interface IUnitOfWork
  {
    Task<bool> Commit();
  }
}

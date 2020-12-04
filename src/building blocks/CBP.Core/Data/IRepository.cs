using CBP.Core.DomainObjects;
using System;

namespace CBP.Core.Data
{
  public interface IRepository<T> : IDisposable where T : IAggregateRoot
  {
    IUnitOfWork UnitOfWork { get; }
  }

}

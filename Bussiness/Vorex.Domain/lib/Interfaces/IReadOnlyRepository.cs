using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.lib.Interfaces;

namespace Vorex.Domain.Interfaces;

public interface IReadOnlyRepository<TEntity> where TEntity : class,IAggregateRoot
{
    IQueryable<TEntity> GetAll(); 

    IQueryable<TEntity> Find(ISpecification<TEntity> specification);
}
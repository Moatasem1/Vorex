using System.Linq.Expressions;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.lib.Interfaces;
namespace Vorex.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class,IAggregateRoot
{
    IEnumerable<TEntity> Find(ISpecification<TEntity> specification);

    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);

    bool Contains(ISpecification<TEntity> specification);
    bool Contains(Expression<Func<TEntity, bool>> predicate);

    int Count(ISpecification<TEntity> specification);
    int Count(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> GetAll();
}

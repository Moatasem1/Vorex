using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib.Interfaces;

namespace Vorex.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IEnumerable<TEntity> Find(ISpecification<TEntity> specification, bool track = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!track)
            query = query.AsNoTracking();

        query = query.Where(specification.Criteria);

        return query.ToList();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public bool Contains(ISpecification<TEntity> specification)
    {
        return _dbSet.Any(specification.Criteria);
    }

    public bool Contains(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }

    public int Count(ISpecification<TEntity> specification)
    {
        return _dbSet.Count(specification.Criteria);
    }

    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Count(predicate);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.AsEnumerable();
    }
}